import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../data.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Event } from "../Event"
import { EventWithID } from '../EventWithID';
import { Customer } from '../Customer';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { Reservation } from '../Reservation';
import { MatStepper } from '@angular/material';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  @ViewChild("stepper", { static: false }) stepper: MatStepper;

  TAX_VALUE = 0.13;

  eventDate: Date;
  resetDate: Date;
  minDate: Date;
  date: Date;
  name: string;
  email: string;
  seats: number;
  eventMaxSeats: number;
  eventName: string;
  displayEventName: string;
  ticketPrice: number;
  event: EventWithID[];
  eventsNoTime: EventWithID[];
  band: Band[];
  eventSeatsSold: EventWithID;
  customer: any;
  existingCustomers: any[];
  currentEvents: EventWithID[];
  paymentProcessing: Boolean;

  testEvents: any[];

  myGroup;

  public payPalConfig?: IPayPalConfig;
  private payPalID = 'AeEiO1jzqkISYf1_qru9moGMmr_QxY6eCZJf3Pgh80jTHWRwxBpO7VNQWdreA9DRZqSk-N0DmX_vgiru';
  showSuccess: boolean;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private data: DataService) {

    this.minDate = new Date();
    this.eventName = null;
    this.name = "";
    this.email = "";
    this.seats = 1;
    this.customer = new Customer();

    this.myGroup = new FormGroup({
      firstName: new FormControl()
   });

    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      this.eventsNoTime = result;
      this.event.sort((a, b) => (a.eventStart > b.eventStart) ? 1 : -1);
      this.currentEvents = this.data.currentEvents(this.event);
    }, error => console.error(error));

    this.http.get<Band[]>(this.baseUrl + 'api/Bands').subscribe(result => {
      this.band = result;
      //console.log(this.band);
    }, error => console.error(error));

    this.getCustomers();

  }

  convertTime = (time24) => {
    const [sHours, minutes] = time24.match(/([0-9]{1,2}):([0-9]{2})/).slice(1);
    const period = +sHours < 12 ? 'AM' : 'PM';
    const hours = +sHours % 12 || 12;

    return `${hours}:${minutes} ${period}`;
  }

  convertCalendarDate = (dateNumbers) => {

    var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    let temp_date = dateNumbers.split("T")[0].split("-");
  return months[Number(temp_date[1]) - 1] + " " + temp_date[2] + " " + temp_date[0];
  }

  convertDate(date){
    return date.split(" ")[0] + " " + date.split(" ")[1] + " " + date.split(" ")[2] + " " + date.split(" ")[3];
  }

  getCustomers(){
    this.http.get<Customer[]>(this.baseUrl + "api/Customers").subscribe(result => {
      console.log(result);
      this.existingCustomers = result;
    }, error => {console.error(error)})
  }

  ngOnInit() {
    this.initConfig();
  }

  updateEventSelected(){
    this.eventSeatsSold = this.filterTime();
  }

  toFixed(num){
    return (+(Math.round(+(num + 'e' + 2)) + 'e' + -2)).toFixed(2);
  }

  private initConfig(): void {
    this.payPalConfig = {
    currency: 'CAD',
    clientId: this.payPalID,
    createOrderOnClient: (data) => <ICreateOrderRequest>{
      intent: 'CAPTURE',
      purchase_units: [
        {
          amount: {
            currency_code: 'CAD',
            value: this.toFixed((this.eventSeatsSold.ticketPrice * this.seats + (this.eventSeatsSold.ticketPrice * this.seats * this.TAX_VALUE))).toString(),
            breakdown: {
              item_total: {
                currency_code: 'CAD',
                value: this.toFixed(this.eventSeatsSold.ticketPrice * this.seats).toString()
              },
              tax_total: {
                currency_code: 'CAD',
                value: this.toFixed(this.eventSeatsSold.ticketPrice * this.seats * this.TAX_VALUE).toString()
              }
            }
          },
          items: [
            {
              name: "Event: " + this.eventSeatsSold.bandName + "     Date : " + this.eventSeatsSold.eventStart,
              quantity: this.seats.toString(),
              category: 'DIGITAL_GOODS',
              unit_amount: {
                currency_code: 'CAD',
                value: this.toFixed(this.eventSeatsSold.ticketPrice).toString()
              },
            }
          ]
        }
      ]
    },
    advanced: {
      commit: 'true'
    },
    style: {
      label: 'paypal',
      layout: 'vertical'
    },
    onApprove: (data, actions) => {
      console.log('onApprove - transaction was approved, but not authorized', data, actions);
      actions.order.get().then(details => {
        console.log('onApprove - you can get full order details inside onApprove: ', details);


      });
    },
    onClientAuthorization: (data) => {
      console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
      this.showSuccess = true;

      //setting values for customer
      this.customer.name = this.name;
      this.customer.email = this.email;
      this.customer.onMailingList = false;

      //if the customer is not an existing customer, add them to the list of customers,
      //and then refresh the list of customers so we can find their ID in the next step.
      if(!this.existingCustomers.find(c => c.email == this.customer.email)){
        this.data.postCustomer(this.customer);

      }

      this.waitUpdatingDatabase().then(() => {
        this.getCustomers();
        console.log("UPDATING EXISTING CUSTOMERS");
        console.log(this.existingCustomers);

        this.waitUpdatingCustomer().then(() => {
          //getting the customers ID from the list of existing customers
          let customerId = this.existingCustomers.find(c => c.email == this.customer.email).id;

          //creating a new reservation object and getting the date/time when the client is placing the order
          var reservation = new Reservation();
          let reservationDate = new Date();

          console.log("RESERVATION DATE");
          console.log(reservationDate);

          //setting the values for the reservation before we post it to reservations DB
          reservation.customerid = customerId;
          reservation.paidInAdvance = true;
          reservation.numberOfSeats = this.seats;
          reservation.resEventid = this.eventSeatsSold.id;
          reservation.timeResMade = reservationDate;
          reservation.paypalId = data.id;

          //inserting the reservation into reservation DB
          this.data.postReservation(reservation);

          //adding seats sold to the current number of seats and then updating
          //the event in the event DB
          this.eventSeatsSold.currentNumberOfSeats += this.seats;
          this.data.editEvent(this.eventSeatsSold.id, this.eventSeatsSold);

          let reservationEmail = {
            email: this.email,
            eventDate: this.formatEventDate(this.eventSeatsSold.eventStart),
            eventName: this.eventSeatsSold.bandName + " $" + this.eventSeatsSold.ticketPrice,
            name: this.customer.name,
            paypalID: data.id,
            purchaseDate: reservationDate.toLocaleString('en-us', {timeZone: 'America/Toronto'}),
            subject: "Receipt # " + data.id,
            ticketPrice: this.eventSeatsSold.ticketPrice.toString(),
            subtotal: data.purchase_units[0].amount.breakdown.item_total.value,
            taxes: data.purchase_units[0].amount.breakdown.tax_total.value,
            totalCost: data.purchase_units[0].amount.value,
            totalSeats: this.seats
          }

          console.log("EVENT SEATS SOLD START TIME");
          console.log(this.eventSeatsSold.eventStart);
          this.formatEventDate(this.eventSeatsSold.eventStart);

          //Sending Reservation Confirmation Email to Client
          this.data.sendReservationEmail(reservationEmail);

          this.date = null;
          this.eventName = "";
          this.name = "";
          this.email = "";
          this.seats = 1;
        });





      });





    },
    onCancel: (data, actions) => {
      console.log('OnCancel', data, actions);
    },
    onError: err => {
      console.log('OnError', err);
    },
    onClick: (data, actions) => {
      console.log('onClick', data, actions);
    },
  };
  }

  waitUpdatingDatabase(){
    return new Promise(vars => setTimeout(vars, 5000))
  }

  waitUpdatingCustomer(){
    return new Promise(vars => setTimeout(vars, 5000))
  }

  formatEventDate(ed){
    let d = ed.split("T")[0];
    let t = ed.split("T")[1];
    let h = t.split(":")[0];
    let m = t.split(":")[1];
    let s = t.split(":")[2];

    let suffix = h >= 12 ? "PM" : "AM";

    return d + " " + ((parseInt(h) + 11) % 12 + 1) + ":" + m + ":" + s + " " + suffix;
  }

  filterEvents(){
    let day = this.date.getDate().toString();
    let month = (this.date.getMonth() + 1).toString();
    if(parseInt(month) < 10)
      month = "0" + month;

    if(parseInt(day) < 10)
      day = "0" + day;

    let filterDate = this.date.getUTCFullYear() + "-" + month + "-" + day;
    //console.log(this.eventsNoTime[10]);
    return this.eventsNoTime.filter(x => x.eventStart.toString().split("T")[0] == filterDate);
  }

  filterTime(){
    return this.event.filter(x => x.eventStart.toString() == this.eventName.toString())[0];
  }

  filterSeats(){
    return this.event.filter(x => x.eventStart.toString().split("T")[1] == this.eventName.toString().split("T")[1]);
  }

  validateName(){
    if(this.name.replace(/[a-zA-Z]+[a-zA-Z- ]*[a-zA-Z]+/g,'').length == 0)
      return false;

    return true;
  }

  validateEmail(){
    if(this.email.replace(/\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b/gi, '').length == 0)
      return false;

    return true;
  }

  resetEventName(){
    this.eventName = null;
  }

  editEvent(){

    /*resetDate: Date;
    minDate: Date;
    date: Date;
    name: string;
    email: string;
    seats: number;
    eventMaxSeats: number;
    eventName: string;
    event: Event[];
    band: Band[];
    */

    // this.createButton = false;
    // this.editButton = true;
    // this.deleteButton = false;
    // this.cancelButton = true;
    // this.eventEditDelete = this.filterOneEvent(this.eventID);
    // this.eventTitle = this.eventEditDelete[0].bandName;
    // this.eventLink = this.eventEditDelete[0].bandLink;
    // this.eventDescription = this.eventEditDelete[0].description;
    // this.date = this.eventEditDelete[0].eventStart;
    // this.endDateTime = this.eventEditDelete[0].eventEnd;
    // this.ticketPrice = this.eventEditDelete[0].ticketPrice;
    // this.maxSeats = this.eventEditDelete[0].maxNumberOfSeats;
    // this.refundCutoffDateTime = this.eventEditDelete[0].refundCutOffDate;
    //this.data.editEvent(this.eventID, this.eventEditDelete);
  }

}

interface Band {
  ID: number,
  BandName: string
  Website: string
  BandInfo: string
}

