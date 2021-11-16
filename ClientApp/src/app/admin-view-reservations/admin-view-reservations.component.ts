import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { EventWithID } from '../EventWithID';
import { Reservation } from '../Reservation';

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
  eventId: Number;

  displayedColumns: string[] = ['Reservation ID', 'Customer ID', 'Seats Reserved', 'Paid In Advance', 'Paypal ID', 'Event ID', 'Time Reservation was Made', 'Customer Name'];

  ngOnInit() {
    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events/EventsWithRes').subscribe(result => {
      this.events = result;
      console.log(this.events);
    }, error => console.error(error));
  }

  GetReservationsBySelectedEventID(id: Number){
    this.http.get<Reservation[]>(this.baseUrl + 'api/Reservations/eId=' + id).subscribe(result => {
      this.reservations = result;
      console.log(this.reservations);
    }, error => console.error(error));
  }

  GetAssociatedCustomer(id: Number){
    this.http.get<any>(this.baseUrl + 'api/Customers/' + id).subscribe(result => {
      return result.name;
    }, error => console.error(error));
  }

  formatEventDate(ed){
    let d = ed.split("T")[0];
    let t = ed.split("T")[1];
    let h = t.split(":")[0];
    let m = t.split(":")[1];
    let s = t.split(":")[2];

    let suffix = h >= 12 ? "PM" : "AM";

    return d + " " + ((parseInt(h) + 11) % 12 + 1) + ":" + m + ":" + s + " " + suffix;
  }

}
