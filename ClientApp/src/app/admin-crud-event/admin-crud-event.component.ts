import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { DataService } from '../data.service';
import { Event } from '../Event'
import { MatDialog } from '@angular/material/dialog';

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

  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,private data: DataService) {

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
    this._snackBar.open(message, "Close", {duration: 5000})
    }

  failureSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

  deleteEvent(){
    this.createButton = false;
    this.editButton = false;
    this.deleteButton = true;
    this.cancelButton = true;
    console.log("Google Cal ID:" + this.eventDelete);
  }

  confirmDelete(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
    this.data.deleteEvent(this.eventDelete);
   // this.openDeleteDialog();
    if(this.confirmDeleteEvent == true){
      
      this.successSnackBar("Event Deleted!");
    }

  }

  openDeleteDialog(){
    const dialogRef = this.dialog.open(DeleteEventDialog);
    dialogRef.afterClosed().subscribe(result => {
      if(result == true){
        this.confirmDeleteEvent = true;
      }
      else{
        this.confirmDeleteEvent = false;
      }
      console.log(`Dialog result: ${result}`);
    })
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

    if(event.bandName != null && event.eventStart != null &&
      event.eventEnd != null && event.maxNumberOfSeats != null &&
      event.ticketPrice != null && event.refundCutOffDate != null){

        //this.openCreateDialog();
        this.data.postEvent(event);
        if(this.confirmCreateEvent == true){
          
          console.log("Creating Event");
          this.successSnackBar("Event Created!");
        }

      }
    else{
      this.failureSnackBar("Please verify all fields with a red '*' are filled out accurately.", "Close");
    }

  }

  openCreateDialog(){
    const dialogRef = this.dialog.open(CreateEventDialog);
    dialogRef.afterClosed().subscribe(result => {
      if(result == true){
        this.confirmCreateEvent = true;
      }
      else{
        this.confirmCreateEvent = false;
      }
      console.log(`Dialog result: ${result}`);
    })
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
    //this.data.getSpecificEvent(this.eventDelete);

    //this.eventTitle = this.data.getSpecificEvent(this.eventDelete);

    console.log(this.data.eventTitle);
    this.setFieldsForEdit();
  }

  confirmEdit(){
    this.createButton = true;
    this.editButton = false;
    this.deleteButton = false;
    this.cancelButton = false;
    this.openEditDialog();
    if(this.confirmDeleteEvent == true){
      //this.data.editEvent(this.eventDelete);
      this.successSnackBar("Event Edited!");
    }
  }

  openEditDialog(){
    const dialogRef = this.dialog.open(EditEventDialog);
    dialogRef.afterClosed().subscribe(result => {
      if(result == true){
        this.confirmDeleteEvent = true;
      }
      else{
        this.confirmDeleteEvent = false;
      }
      console.log(`Dialog result: ${result}`);
    })
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

@Component({
  selector: 'create-event-dialog',
  templateUrl: './create-event-dialog.html',
})
export class CreateEventDialog{}

@Component({
  selector: 'delete-event-dialog',
  templateUrl: './delete-event-dialog.html',
})
export class DeleteEventDialog{}

@Component({
  selector: 'edit-event-dialog',
  templateUrl: './edit-event-dialog.html',
})
export class EditEventDialog{}
