import { Component, OnInit, Inject } from '@angular/core';
import { DataService } from '../data.service';
import { HttpClient } from '@angular/common/http';
import { EventWithID } from '../EventWithID';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  event: EventWithID[];

  constructor(private data: DataService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<EventWithID[]>(this.baseUrl + 'api/Events').subscribe(result => {
      this.event = result;
      this.event.sort((a, b) => (a.eventStart > b.eventStart) ? 1 : -1);
      console.log(this.event);
    }, error => console.error(error));
  }

  ngOnInit() {
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
