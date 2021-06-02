import { Component, OnInit, enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment, firebaseConfig } from './environments/environment';
import firebase from "firebase/app" //importing main functionality

var currentPage = [];

@Component({
  selector: 'app-root',
  templateUrl: './index.html',
  styleUrls: ['./styles.css']
})
export class MainComponent implements OnInit{

  adminCssUrl: string;


  constructor(){

    //console.log(this.router.url);

  }

  ngOnInit(){
    currentPage[0] = window.location.href;
    this.adminCssUrl = './styles_admin.css';
  }

}

export function getBaseUrl() {
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
