import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import firebase from "firebase/app";
import "firebase/auth";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  provider: any;
  user: any;
  email: string;
  msg: string;
  isAdmin: boolean = false;
  dbIsConnected: boolean = true;
  tempAdmin: any;
  testEvent : Event;
  events: any[];
  eventIds: any[];


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  ngOnInit(): void {
    var provider = new firebase.auth.GoogleAuthProvider(); //declaring provider
    this.provider = provider;
    this.events = this.provider.addScope('https://www.googleapis.com/auth/calendar');

    this.checkDBConnect();

    this.testEvent = {
      cId : "primary",
      eventStart: new Date("2021-06-26"),
      eventEnd: new Date("2021-06-27")
    }

  }

  loginWithGmail() {
    firebase.auth().signInWithPopup(this.provider)
      .then((result) => {
        /** @type {firebase.auth.OAuthCredential} */
        var credential = result.credential;

        this.user = result.user;
        this.email = result.user.email;

        this.newAuthCheck();
      }).catch((error) => {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });

  }

  // GetCalendarEventIds(){
  //   this.http.get<Event[]>(
  //     "https://www.googleapis.com/calendar/v3/users/me/calendarList.list"
  //     ).subscribe(result => {
  //     this.eventIds = result;
  //     console.log(this.eventIds);
  //   }, error => console.error(error));
  // }

  // CreateTestEvent(){
  //   this.http.get<Event[]>(
  //     "https://www.googleapis.com/calendar/v3/calendars/primary/events.readonly"
  //     ).subscribe(result => {
  //     this.events = result;
  //     console.log(this.events);
  //   }, error => console.error(error));
  // }

  newAuthCheck() {
    this.msg = "You are not authorized!";
    this.http.get<Admin[]>(this.baseUrl + 'api/Admins/email=' + this.email).subscribe(result => {
      this.tempAdmin = result;
      console.log(this.tempAdmin);
      this.isAdmin = true;
      this.msg = "You are an admin!";
    }, error => console.error(error));
  }

  checkDBConnect() {
    if (!this.dbIsConnected) {
      this.msg = "Cannot authorize at this time, please try again later";
    }
  }

}

interface Admin {
  ID: number,
  name: string,
  email: string,
  phoneNumber: string,
  accessLevel: number
}

interface Event{
  cId: string,
  eventStart: Date,
  eventEnd: Date,
}
