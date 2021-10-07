import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import * as data from '../assets/data/events.json';
import { Admin } from '../app/Admin';
import { Event } from './Event';
import { EventWithID } from './EventWithID';

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

  postEvent(event: Event){
    var postData: any;
    this.http.post<Event[]>(this.baseUrl + "api/Events", event).subscribe(data => postData = data);
  }

  deleteEvent(id: Number){
    var callResult : any;
    this.http.delete(this.baseUrl + 'api/Events/' + id).subscribe(result =>{
      callResult = result;
      console.log(callResult);
    }, error => {console.error(error)});
  }

  editEvent(id: Number, event: EventWithID[]){
    var callResult : any;
    console.log(id);
    console.log(event);
    this.http.put(this.baseUrl + 'api/Events/' + id , event).subscribe(result => {
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
    this.http.post(this.baseUrl + 'api/AdminSendEmail', email).subscribe(data => postData = data);
  }

  logOut(){

  }
}

interface Email {
  emailSubject: String;
  emailBody: String;
  emailImage: String;
}
