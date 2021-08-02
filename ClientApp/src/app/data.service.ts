import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as data from '../assets/data/events.json';
import { Admin } from '../app/Admin'

@Injectable({
  providedIn: 'root'
})
export class DataService {

  event: any[];
  private tempAdminCheck = new BehaviorSubject('');
  currentCheck = this.tempAdminCheck.asObservable();
  //isAdmin: boolean = false;
  adminObject: any;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.event = data.Events;
   }

  async getEmail(email: string): Promise<boolean>{ //would like to implement hash function for comparison on the backend in the future
    var isAdmin: boolean
    await this.http.get<Admin[]>(this.baseUrl + 'api/Admins/email=' + email).subscribe(result => {
      this.adminObject = result;
      console.log(this.adminObject);
      isAdmin = true;
      console.log(isAdmin)
    }, error => console.error(error));
    return isAdmin;
  }

  setTempAdmin(message: string){
    this.tempAdminCheck.next(message);
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

