import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Event } from '../Event'

@Component({
  selector: 'app-admin-crud-event',
  templateUrl: './admin-crud-event.component.html',
  styleUrls: ['./admin-crud-event.component.css']
})
export class AdminCrudEventComponent implements OnInit {
  testEvent: Event;

  eventTitle: String;
  eventLink: String;
  eventImage: String;
  eventDescription: String;
  startDateTime: Date;
  endDateTime: Date;
  ticketPrice: number;
  maxSeats: number;
  refundCutoffDateTime: Date;

  constructor(private data: DataService) {

  }

  ngOnInit() {
  }

  deleteEvent(){
    this.data.deleteEvent("testID");
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
      googleCalID: "56"
    }

    this.data.postEvent(event);

  }

  editEvent(){
    console.log("Create Event");
  }

}
