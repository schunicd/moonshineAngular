import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs/internal/operators/delay';
import { DataService } from '../data.service';
import { Event } from '../Event'

@Component({
  selector: 'app-admin-crud-event',
  templateUrl: './admin-crud-event.component.html',
  styleUrls: ['./admin-crud-event.component.css']
})
export class AdminCrudEventComponent implements OnInit {
  testEvent: Event;
  eventToEdit: Event[];

  eventTitle: String;
  eventLink: String;
  eventImage: String;
  eventDescription: String;
  startDateTime: Date;
  endDateTime: Date;
  ticketPrice: number;
  maxSeats: number;
  refundCutoffDateTime: Date;
  dateDelete: Date;
  eventDelete: String;
  event: Event[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,private data: DataService) {

    this.http.get<Event[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));

  }

  ngOnInit() {
  }

  deleteEvent(){
    console.log(this.eventDelete);
    this.data.deleteEvent(this.eventDelete);
  }

  resetEventName(){
    this.eventDelete = null;
  }

  createEvent(){

    let event = {
      eventStart: this.startDateTime,
      eventEnd: this.endDateTime,
      refundCutOffDate: this.refundCutoffDateTime,
      bandName: this.eventTitle,
      bandImagePath: this.eventImage,
      bandLink: this.eventLink,
      maxNumberOfSeats: this.maxSeats,
      currentNumberOfSeats: 0,
      ticketPrice: this.ticketPrice,
      description: this.eventDescription,
      googleCalID: "abc123"
    }

    this.data.postEvent(event);

  }

  filterEvents(){

    let day = this.dateDelete.getDate().toString();
    let month = (this.dateDelete.getMonth() + 1).toString();
    if(parseInt(month) < 10)
      month = "0" + month;

    if(parseInt(day) < 10)
      day = "0" + day;

    let filterDate = this.dateDelete.getUTCFullYear() + "-" + month + "-" + day;
    return this.event.filter(x => x.eventStart.toString().split("T")[0] == filterDate);
  }

  editEvent(){
    console.log("Edit Event");
    this.data.getSpecificEvent(this.eventDelete);

    console.log(this.data.eventTitle);
    this.setFieldsForEdit();
  }

  setFieldsForEdit(){
    this.eventTitle = this.data.eventTitle;
  }

}
