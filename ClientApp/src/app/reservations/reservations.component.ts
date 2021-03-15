import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {

  minDate: Date;
  date: Date;
  name: string;
  email: string;
  seats: number;
  eventName: string;
  event: Event[];
  band: Band[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.minDate = new Date();
    this.name = "";
    this.email = "";

    console.log(this.minDate);

    http.get<Event[]>(baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));

    http.get<Band[]>(baseUrl + 'api/Bands').subscribe(result => {
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

  tentativeBooking(){}

  paidBooking(){
    console.log(this.eventName);
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

