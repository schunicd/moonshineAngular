import { Injectable } from '@angular/core';
import * as data from '../assets/data/events.json';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  event: any[];


  constructor() {
    this.event = data.Events;
   }

}

interface Event {
  id: number;
  eventDate: Date;
  bandId: number;
  maxSeats: number;
  currentSeats: number;
  ticketPrice: number;
}
