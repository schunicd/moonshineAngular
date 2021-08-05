import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import * as data from '../assets/data/events.json';
import { Admin } from '../app/Admin'

@Injectable({
  providedIn: 'root'
})
export class DataService {

  event: any[];
  private tempAdminCheck = new BehaviorSubject('');
  currentCheck = this.tempAdminCheck.asObservable();
  private isAdmin: boolean;
  adminObject: any;


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
      console.log("In await: " + this.isAdmin)
    }, error => {console.error(error)});
    console.log(this.isAdmin)
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

interface Event {
  id: number;
  eventDate: Date;
  bandId: number;
  maxSeats: number;
  currentSeats: number;
  ticketPrice: number;
}

