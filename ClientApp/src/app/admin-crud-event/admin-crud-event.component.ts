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
  
  constructor(private data: DataService) {}

  ngOnInit() {
  }

  deleteEvent(){
    this.data.deleteEvent("testID");
  }

  createEvent(){
    let event = {
      id: null,
      eventStart: new Date,
      eventEnd: new Date,
      refundDate: new Date,
      bandName: "Led Zeppelin",
      bandImagePath: "asd",
      bandLink: "asasddd",
      maxSeats: 1,
      currentSeats: 20, 
      ticketPrice: 10,
      description: "it's a band",
      googleCalendarID: "aksjdhfkjasdhf"
    }

    this.data.postEvent(event);
  }

  editEvent(){
    console.log("Create Event");
  }

}
