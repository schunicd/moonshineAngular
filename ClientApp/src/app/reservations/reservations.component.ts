import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {

  date: any;
  event: Event[];
  band: Band[];
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<Event[]>(baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      console.log(this.event);
    }, error => console.error(error));

    http.get<Band[]>(baseUrl + 'api/Bands').subscribe(result => {
      this.band = result;
      console.log(this.band);
    }, error => console.error(error));



  }

  ngOnInit() {

  }

}

interface Band {
  ID: number,
  BandName: string 
  Website: string  
  BandInfo: string  
}

interface Event {
  id: number;
  eventDate: Date;
  bandId: number;
  maxSeats: number;
  currentSeats: number;
  ticketPrice: number;
}

