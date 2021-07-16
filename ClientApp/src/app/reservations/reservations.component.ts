import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../data.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {

  resetDate: Date;
  minDate: Date;
  date: Date;
  name: string;
  email: string;
  seats: number;
  eventMaxSeats: number;
  eventName: string;
  event: Event[];
  band: Band[];

  myGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private data: DataService) {

    this.minDate = new Date();
    this.eventName = null;
    this.name = "";
    this.email = "";

    console.log(this.minDate);

    this.myGroup = new FormGroup({
      firstName: new FormControl()
   });

    this.http.get<Event[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));

    this.http.get<Band[]>(this.baseUrl + 'api/Bands').subscribe(result => {
      this.band = result;
      console.log(this.band);
    }, error => console.error(error));

  }

  ngOnInit() {

  }

  filterEvents(){

    let day = this.date.getDate().toString();
    let month = (this.date.getMonth() + 1).toString();
    if(parseInt(month) < 10)
      month = "0" + month;

    if(parseInt(day) < 10)
      day = "0" + day;

    let filterDate = this.date.getUTCFullYear() + "-" + month + "-" + day;

    return this.event.filter(x => x.eventDate.toString().split("T")[0] == filterDate);
  }

  filterSeats(){
    return this.event.filter(x => x.eventDate.toString() == this.eventName);
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

  tentativeBooking(){
    if(confirm("Confirm Tentative Booking On: " + this.eventName)){
      console.log("Tentative Booking Confirmed!");
      this.name = "";
      this.date = this.resetDate;
      this.email = "";
      this.seats = 0;
      this.eventName = null;
      return;
    }
    console.log("Tentative Booking Cancelled!");
  }

  paidBooking(){
    let maxseats = this.filterSeats();
    if(confirm("Confirm Paid Booking On: " + this.eventName)){
      console.log("Paid Booking Confirmed!");

      let postData;

      let reservation = {
        paidInAdvance: true,
        timeResMade: new Date,
        customerid: 43,
        numberOfSeats: this.seats,
        resEventid: 75
      }

      console.log("RESERVATION");
      console.log(reservation);
      this.http.post<Reservation[]>(this.baseUrl + "api/Reservations/", reservation).subscribe(data => postData = data);
      console.log("POST DATA");
      console.log(postData);
      this.name = "";
      this.date = this.resetDate;
      this.email = "";
      this.seats = 0;
      this.eventName = null;
      return;
    }


    console.log("Paid Booking Canceled!");
  }

}

interface Band {
  ID: number,
  BandName: string
  Website: string
  BandInfo: string
}

interface Event {
  id: number;
  eventDate: Date;
  bandId: number;
  maxSeats: number;
  currentSeats: number;
  ticketPrice: number;
}

interface Reservation {
  id: number,
  paidInAdvance: boolean,
  timeResMade: Date,
  customerid: number,
  numberOfSeats: number,
  resEventid: number
}
