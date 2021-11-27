import { Component, OnInit, Inject } from '@angular/core';
import { DataService } from '../data.service';
import { Customer } from '../Customer';
import { HttpClient } from '@angular/common/http';
import { EventWithID } from '../EventWithID';

export interface Tile {
  cols: number;
  rows: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  panelOpenState = false;
  event: EventWithID[];
  currentEvents: EventWithID[];

  constructor(private data: DataService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      this.event.sort((a, b) => (a.eventStart > b.eventStart) ? 1 : -1);
      this.currentEvents = this.data.currentEvents(this.event);
    }, error => console.error(error));



    this.http.get<any[]>(this.baseUrl + 'api/Events/Calendar').subscribe(result => {
    }, error => console.error(error));

   }

   getMailingListClients(){
    this.http.get<Customer[]>(this.baseUrl + "api/Customers").subscribe(result => {
      this.existingCustomers = result;
    }, error => {console.error(error)})
   }

  leftFirstTile: Tile = {cols: 1, rows: 1};
  leftSecondTile: Tile = {cols: 1, rows: 1};
  leftThirdTile: Tile = {cols: 1, rows: 4};
  leftFourthTile: Tile = {cols: 1, rows: 2};
  rightFirstTile: Tile = {cols: 1, rows: 3};
  rightSecondTile: Tile = {cols: 1, rows: 4};
  bottomTile: Tile = {cols: 2, rows: 0.5};

  existingCustomers: any[];
  clientEmail: string;
  newEmail = new Customer();

  joinMailingList(){

    this.getMailingListClients();

    console.log(this.clientEmail);
    console.log(this.existingCustomers);

    let customerCheck = this.existingCustomers.filter(obj => {return obj.email == this.clientEmail});
    console.log(customerCheck);

    //if customer is not in the customer database, add customer to database with default name
    if(customerCheck.length < 1 ){
      this.newEmail.name = "Email List Client";
      this.newEmail.email = this.clientEmail;
      this.newEmail.onMailingList = true;
      this.data.postCustomer(this.newEmail);
    }
    else if(customerCheck[0].onMailingList == false) //else if customer IS in customer database and not on the mailing list
    {
      customerCheck[0].onMailingList = true;
      this.data.editCustomer(customerCheck[0].id, customerCheck[0]);
    }

  }

  convertTime = (time24) => {
    const [sHours, minutes] = time24.match(/([0-9]{1,2}):([0-9]{2})/).slice(1);
    const period = +sHours < 12 ? 'AM' : 'PM';
    const hours = +sHours % 12 || 12;

    return `${hours}:${minutes} ${period}`;
  }

  convertDate = (dateNumbers) => {

    var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    let temp_date = dateNumbers.split("T")[0].split("-");
  return months[Number(temp_date[1]) - 1] + " " + temp_date[2] + " " + temp_date[0];
  }

}
