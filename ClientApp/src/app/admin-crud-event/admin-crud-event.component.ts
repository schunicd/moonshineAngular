import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
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
  confirmCreateEvent: boolean;
  confirmDeleteEvent: boolean;
  confirmEditEvent: boolean;
  createButton: boolean;
  editButton: boolean;
  deleteButton: boolean;
  cancelButton: boolean;

  eventID: Number;
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

  constructor(private _snackBar: MatSnackBar, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private data: DataService) {

    this.http.get<Event[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));

  }

  ngOnInit() {
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

  deleteEvent(){
    this.createButton = false;
    this.editButton = false;
    this.deleteButton = true;
    this.cancelButton = true;
  }

  confirmDelete(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
    console.log(this.eventID);
    this.data.deleteEvent(this.eventID);
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
      }
    else{
      this.failureSnackBar("Please verify all fields with a red '*' are filled out accurately.", "Close");
    }

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

    this.createButton = false;
    this.editButton = true;
    this.deleteButton = false;
    this.cancelButton = true;

    console.log("Edit Event");

    console.log(this.data.eventTitle);
    this.setFieldsForEdit();
  }

  confirmEdit(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
    this.data.editEvent(this.eventID);
  }

  cancelEditDelete(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
  }

  setFieldsForEdit(){
    this.eventTitle = this.data.eventTitle;
  }

}