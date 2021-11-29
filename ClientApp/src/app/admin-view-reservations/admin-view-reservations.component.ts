import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { EventWithID } from '../EventWithID';
import { Reservation } from '../Reservation';
import { Customer } from '../Customer';

@Component({
  selector: 'app-admin-view-reservations',
  templateUrl: './admin-view-reservations.component.html',
  styleUrls: ['./admin-view-reservations.component.css']
})
export class AdminViewReservationsComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  events: EventWithID[];
  selectedEvent: EventWithID;
  reservations: Reservation[];
  customers: Customer[] = [];
  eventId: Number;
  delete: Boolean = false;

  displayedColumns: string[] = ['Customer Name', 'Seats Reserved', 'Paypal ID', 'Time Reservation was Made', 'Delete'];

  ngOnInit() {
    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events/EventsWithRes').subscribe(result => {
      this.events = result;
      console.log(this.events);
    }, error => console.error(error));
  }

  GetReservationsBySelectedEventID(id: Number){
    this.http.get<Reservation[]>(this.baseUrl + 'api/Reservations/eId=' + id).subscribe(result => {
      this.reservations = result;
      this.reservations.forEach(r => {
        this.GetAssociatedCustomer(r.customerid);
      });
      console.log(this.reservations);
    }, error => console.error(error));
  }

  toggleDelete(){
    if(this.delete)
      this.delete = false;
    else
      this.delete = true;
  }

  GetAssociatedCustomer(id: Number){
    this.http.get<Customer>(this.baseUrl + 'api/Customers/' + id).subscribe(result => {
      if(!this.customers.find(customer => customer == result))
      this.customers.push(result);
      console.log(result);
      console.log(this.customers);
    }, error => console.error(error));
  }

  findSpecificCustomer(id: Number){
    let name = this.customers.filter(cust => {return cust.id == id})[0].name;
    return name;
  }

  formatEventDate(ed){
    let d = ed.split("T")[0];
    let t = ed.split("T")[1];
    let h = t.split(":")[0];
    let m = t.split(":")[1];
    let s = t.split(":")[2];
    s = s.split(".")[0];

    let suffix = h >= 12 ? "PM" : "AM";

    return d + " " + ((parseInt(h) + 11) % 12 + 1) + ":" + m + ":" + s + " " + suffix;
  }

}
