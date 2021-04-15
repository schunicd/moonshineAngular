import { Component, OnInit, enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

import { AppModule } from './app/app.module';
import { environment, firebaseConfig } from './environments/environment';
import firebase from "firebase/app" //importing main functionality

var currentPage = [];
var curPageString = "";
var curPageAdmin = false;

@Component({
  selector: 'app-root',
  templateUrl: './index.html',
  styleUrls: ['./styles.css']
})
export class MainComponent implements OnInit{

  adminCssUrl: string;


  constructor(private router: Router, private sanitizer: DomSanitizer){

    //console.log(this.router.url);

  }

  ngOnInit(){
    currentPage[0] = window.location.href;
    this.adminCssUrl = './styles_admin.css';
  }

}

export function getBaseUrl() {
  console.log("NGONINIT");
  console.log(window.location.href);
  currentPage[0] = window.location.href.split('/', 5);
  console.log(currentPage);
  curPageString = currentPage[0][3];
  console.log(curPageString);
  curPageAdmin = curPageString == "admin";
  console.log(curPageAdmin);
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
  firebase.initializeApp(firebaseConfig);
