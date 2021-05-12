import { Component } from '@angular/core';

var currentPage = [];
var curPageString = "";
var curPageAdmin = false;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(){
    console.log("NGONINIT");
    console.log(window.location.href);
    currentPage[0] = window.location.href.split('/', 5);
    console.log(currentPage);
    curPageString = currentPage[0][3];
    console.log(curPageString);
    curPageAdmin = curPageString == "admin";
    console.log(curPageAdmin);
  }

}
