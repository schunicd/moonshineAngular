import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-admin-crud-event',
  templateUrl: './admin-crud-event.component.html',
  styleUrls: ['./admin-crud-event.component.css']
})
export class AdminCrudEventComponent implements OnInit {

  constructor(private data: DataService) { }

  ngOnInit() {
  }

  deleteEvent(){
    this.data.deleteEvent("testID");
  }
  // deleteEvent(calID: String){
  //   this.data.deleteEventByDate(calID);
  // }

  createEvent(){
    console.log("Create Event");
  }

  editEvent(){
    console.log("Create Event");
  }

}
