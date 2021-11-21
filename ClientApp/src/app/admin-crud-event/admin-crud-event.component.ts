import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../data.service';
import { Event } from '../Event';
import { EventWithID } from '../EventWithID';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-admin-crud-event',
  templateUrl: './admin-crud-event.component.html',
  styleUrls: ['./admin-crud-event.component.css']
})
export class AdminCrudEventComponent implements OnInit {
  testEvent: Event;
  eventToEdit: Event[];
  confirmCreateEvent: boolean;
  confirmDeleteEvent: boolean;
  confirmEditEvent: boolean;
  createButton: boolean;
  editButton: boolean;
  deleteButton: boolean;
  cancelButton: boolean;

  eventID: number;
  googleCalID: String;
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
  event: EventWithID[];
  eventEditDelete: EventWithID;
  stepperIndex: number;
  eventToUpdate: updateEvent;

  constructor(private _snackBar: MatSnackBar, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private data: DataService) {

  }

  getEvents(){
    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));
  }

  ngOnInit() {
    this.getEvents();
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
  }

  successSnackBar(message: string) {
    this._snackBar.open(message, "Close", {duration: 5000});
  }

  failureSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

  clearForm(){
    this.eventTitle = "";
    this.eventLink = "";
    this.eventImage = "";
    this.eventDescription = "";
    this.startDateTime = null;
    this.endDateTime = null;
    this.ticketPrice = null;
    this.maxSeats = null;
    this.refundCutoffDateTime = null;
    this.eventID = null;
    this.dateDelete = null;
    this.stepperIndex = 0;
  }

  deleteEvent(){
    this.createButton = false;
    this.editButton = false;
    this.deleteButton = true;
    this.cancelButton = true;
    this.eventEditDelete = this.filterOneEvent(this.eventID)[0];
    this.eventTitle = this.eventEditDelete.bandName;
    this.eventLink = this.eventEditDelete.bandLink;
    this.eventDescription = this.eventEditDelete.description;
    this.startDateTime = this.eventEditDelete.eventStart;
    this.endDateTime = this.eventEditDelete.eventEnd;
    this.ticketPrice = this.eventEditDelete.ticketPrice;
    this.maxSeats = this.eventEditDelete.maxNumberOfSeats;
    this.refundCutoffDateTime = this.eventEditDelete.refundCutOffDate;
    this.googleCalID = this.eventEditDelete.googleCalID;
  }

  confirmDelete(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;

    console.log(this.eventID);
    this.data.deleteEvent(this.googleCalID);
    this.clearForm();
    this.successSnackBar("Event Deleted!");
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
    }

    if(event.bandName != null && event.eventStart != null &&
      event.eventEnd != null && event.maxNumberOfSeats != null &&
      event.ticketPrice != null && event.refundCutOffDate != null){

        this.data.postEvent(event);
        console.log(event);
        this.clearForm();
        this.successSnackBar("Event Created!");
      }

    else{
      this.failureSnackBar("Please verify all fields with a red '*' are filled out accurately.", "Close");
    }
  }

  filterEvents(){
    this.getEvents();
    let day = this.dateDelete.getDate().toString();
    let month = (this.dateDelete.getMonth() + 1).toString();
    if(parseInt(month) < 10)
      month = "0" + month;

    if(parseInt(day) < 10)
      day = "0" + day;

    let filterDate = this.dateDelete.getUTCFullYear() + "-" + month + "-" + day;
    return this.event.filter(x => x.eventStart.toString().split("T")[0] == filterDate);
  }

  filterOneEvent(id: number){
    return this.event.filter(x => x.id == id);
  }

  editEvent(){
    this.createButton = false;
    this.editButton = true;
    this.deleteButton = false;
    this.cancelButton = true;
    this.eventEditDelete = this.filterOneEvent(this.eventID)[0];
    this.eventTitle = this.eventEditDelete.bandName;
    this.eventImage = "";
    this.eventLink = this.eventEditDelete.bandLink;
    this.eventDescription = this.eventEditDelete.description;
    this.startDateTime = this.eventEditDelete.eventStart;
    this.endDateTime = this.eventEditDelete.eventEnd;
    this.ticketPrice = this.eventEditDelete.ticketPrice;
    this.maxSeats = this.eventEditDelete.maxNumberOfSeats;
    this.refundCutoffDateTime = this.eventEditDelete.refundCutOffDate;
    this.googleCalID = this.eventEditDelete.googleCalID;
  }

  confirmEdit(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
    this.eventEditDelete.bandName = this.eventTitle;
    this.eventEditDelete.bandImagePath = this.eventImage;
    this.eventEditDelete.bandLink = this.eventLink;
    this.eventEditDelete.description = this.eventDescription;
    this.eventEditDelete.eventStart = this.startDateTime;
    this.eventEditDelete.eventEnd = this.endDateTime;
    this.eventEditDelete.ticketPrice = this.ticketPrice;
    this.eventEditDelete.maxNumberOfSeats = this.maxSeats;
    this.eventEditDelete.refundCutOffDate = this.refundCutoffDateTime;
    this.data.editEvent(this.eventID, this.eventEditDelete);
    
    this.clearForm();
    this.successSnackBar("Event Edited!");
  }

  cancelEditDelete(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
  }

}

interface updateEvent{
  id: string;
  description: string;
}
