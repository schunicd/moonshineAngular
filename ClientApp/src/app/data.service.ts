import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import * as data from '../assets/data/events.json';
import { Admin } from '../app/Admin'
import { Event } from './Event'

@Injectable({
  providedIn: 'root'
})
export class DataService {

  event: any[];
  eventToEdit: Event[];
  private tempAdminCheck = new BehaviorSubject('');
  currentCheck = this.tempAdminCheck.asObservable();
  private isAdmin: boolean;
  adminObject: any;

  eventTitle: String;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.event = data.Events;
    this.isAdmin = false;
    console.log("New service instantiated")
   }

  async getEmail(email: string){ //would like to implement hash function for comparison on the backend in the future
    await this.http.get<Admin[]>(this.baseUrl + 'api/Admins/email=' + email).subscribe(result => {
      this.adminObject = result;
      console.log(this.adminObject);
      this.isAdmin = true;
    }, error => {console.error(error)});
    console.log(this.isAdmin)
  }

  async getCalEvents(){
    console.log("Angular function called");
    await this.http.get<any[]>(this.baseUrl + 'api/Events/Calendar').subscribe(result => {
      console.log(result);
    }, error => {console.error(error)})
  }

  postEvent(event: Event){
    var postData: any;
    this.http.post<Event[]>(this.baseUrl + "api/Events", event).subscribe(data => postData = data);
    //this.http.post<Reservation[]>(this.baseUrl + "api/Reservations/", reservation).subscribe(data => postData = data);
  }

  async deleteEvent(calID: String){
    var callResult : any;
    await this.http.delete(this.baseUrl + 'api/Events/' + calID).subscribe(result =>{
      callResult = result;
      console.log(callResult);
    }, error => {console.error(error)});
  }

  async editEvent(calID: String){
    var callResult : any;
    await this.http.put(this.baseUrl + 'api/Events/' + calID , this.getSpecificEvent).subscribe(result => {
      callResult = result;
    })
  }

  public async getSpecificEvent(calID: String){
    await this.http.get(this.baseUrl + 'api/Events/calID=' + calID).subscribe((result : Event) => {
      console.log(result);
      this.eventTitle = result.bandName;
    }, error => {console.error(error)})
  }

  setTempAdmin(message: string){
    this.tempAdminCheck.next(message);
  }

  getIsAdmin(): boolean{
    return this.isAdmin;
  }

  logOut(){

  }
}

