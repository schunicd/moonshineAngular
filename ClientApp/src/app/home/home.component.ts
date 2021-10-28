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

  result: any[]

  constructor(private data: DataService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    this.http.get<Customer[]>(this.baseUrl + "api/Customers").subscribe(result => {
      //console.log(result);
      this.existingCustomers = result;
    }, error => {console.error(error)})

    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events/upcoming').subscribe(result => {
      this.result = result;
      console.log(this.result);
    }, error => console.error(error));
    console.log(this.result)

   }
   
   ngOnInit(){
    //console.log(this.getUpcomingEvents());
   }

  async getUpcomingEvents(){
    await this.http.get<EventWithID[]>(this.baseUrl + 'api/Events/upcoming').subscribe(result => {
      this.result = result;
      const test = result;
      return test
    }, error => console.error(error));
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
    console.log(this.clientEmail);
    console.log(this.existingCustomers);

    let customerCheck = this.existingCustomers.filter(obj => {return obj.email == this.clientEmail});
    console.log(customerCheck);

    if(customerCheck.length < 1 ){
      this.newEmail.name = "Email List Client";
      this.newEmail.email = this.clientEmail;
      this.newEmail.onMailingList = true;
      this.data.postCustomer(this.newEmail);
    }
    else if(customerCheck[0].onMailingList == false)
    {
      customerCheck[0].onMailingList = true;
      this.data.editCustomer(customerCheck[0].id, customerCheck[0]);
    }

  }

}
