import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpRequest, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import * as data from '../assets/data/events.json';
import { Admin } from '../app/Admin';
import { Event } from './Event';
import { EventWithID } from './EventWithID';
import { Customer } from './Customer';
import { Reservation } from './Reservation';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  event: any[];
  eventToEdit: Event[];
  private tempAdminCheck = new BehaviorSubject('');
  currentCheck = this.tempAdminCheck.asObservable();
  public isAdmin: boolean;
  adminObject: any;
  existingCustomers: any[];

  eventTitle: String;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.event = data.Events;
    this.isAdmin = false;
    console.log("New service instantiated")
   }

  getEmail(email: string){ //would like to implement hash function for comparison on the backend in the future
    this.http.get<Admin[]>(this.baseUrl + 'api/Admins/email=' + email).subscribe(result => {
      this.adminObject = result;
      console.log(this.adminObject);
      this.isAdmin = true;
    }, error => {console.error(error)});
    console.log(this.isAdmin)
  }

  getCalEvents(){
     this.http.get<any[]>(this.baseUrl + 'api/Events').subscribe(result => {
      console.log(result);
    }, error => {console.error(error)})
  }

  currentEvents(events: EventWithID[]){
    let today = new Date();
    var currentEvents: EventWithID[] = [];
    today.setHours(0,0,1,0);
    events.forEach(e => {
      if(e.eventStart.toString() >= today.toISOString())
      {
        currentEvents.push(e);
      }
    });
    return currentEvents;
  }

  postEvent(event: Event){
    var postData: any;
    this.http.post<Event[]>(this.baseUrl + "api/Events", event).subscribe(data => postData = data);
  }

  uploadBandImage(file: File){
    const formData = new FormData();

    formData.append(file.name, file);
    var postData: any;
    const uploadReq = new HttpRequest('POST', `api/imageUpload/AddBandImage/`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {});

  }

  getCustomers(){
    this.http.get<Customer[]>(this.baseUrl + "api/Customers").subscribe(result => {
      console.log(result);
      this.existingCustomers = result;
    }, error => {console.error(error)})
  }

  async postCustomer(customer: Customer){
    var postData: any;
    await this.http.post<Customer>(this.baseUrl + "api/Customers", customer).subscribe(data => {
      console.log(data);
    }, error => {console.error(error)});
  }

  postReservation(res: Reservation){
    var postData: any;
    this.http.post<Reservation>(this.baseUrl + "api/Reservations", res).subscribe(data => {
      console.log(data);
    }, error => {console.error(error)});
  }

  editCustomer(id: Number, customer: Customer){
    var callResult : any;
    console.log(id);
    console.log(customer);
    this.http.put<Customer>(this.baseUrl + 'api/Customers/' + id , customer).subscribe(result => {
      callResult = result;
      console.log(result);
    })
  }


  deleteEvent(calID: String){
    var callResult : any;
    this.http.delete(this.baseUrl + 'api/Events/' + calID).subscribe(result =>{
      callResult = result;
      console.log(callResult);
    }, error => {console.error(error)});
  }

  editEvent(id: Number, event: EventWithID){
    var callResult : any;
    console.log(id);
    console.log(event);
    this.http.put<EventWithID>(this.baseUrl + 'api/Events/' + id , event).subscribe(result => {
      callResult = result;
      console.log(result);
    })
  }


  /*
  async getSpecificEvent(id: Number): Promise<Event>{
    var event;
    await this.http.get(this.baseUrl + 'api/Events/' + id).subscribe((result : Event) => {
      console.log(result);
      event = result;
      this.eventTitle = result.bandName;
    }, error => {console.error(error)})
    return event;
  }
  */

  setTempAdmin(message: string){
    this.tempAdminCheck.next(message);
  }

  getIsAdmin(): boolean{
    return this.isAdmin;
  }

  sendEmail(email: Email){
    var postData: any;
    console.log(email);
    this.http.post(this.baseUrl + 'api/AdminSendEmail', email).subscribe(data => postData = data);
  }

  sendReservationEmail(email: ReservationEmail){
    var postData: any;
    console.log(email)
    this.http.post(this.baseUrl + 'api/AdminSendEmail/Reservation', email).subscribe(data => postData = data);
  }

  logOut(){

  }
}

interface Email {
  subject: String;
  body: String;
  image: String;
}

interface ReservationEmail {
  subject: String;
  eventDate: String;
  eventName: String;
  name: String;
  email: String;
  purchaseDate: String;
  totalSeats: Number;
  ticketPrice: String;
  subtotal: String;
  taxes: String;
  totalCost: String;
  paypalID: String;
}
