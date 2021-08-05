import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from '../data.service';
import firebase from "firebase/app";
import "firebase/auth";
import { Admin } from '../Admin';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  provider: any;
  user: any;
  email: string;
  msg: string;
  isAdmin: boolean = false;
  dbIsConnected: boolean = true;
  tempAdmin: any;


  constructor(private router: Router, private route: ActivatedRoute, private data: DataService) {

  }

  ngOnInit(): void {
    var provider = new firebase.auth.GoogleAuthProvider(); //declaring provider
    this.provider = provider;

    this.checkDBConnect();

  }

  loginWithGmail() {
    firebase.auth().signInWithPopup(this.provider)
      .then((result) => {
        /** @type {firebase.auth.OAuthCredential} */
        var credential = result.credential;

        this.user = result.user;
        this.email = result.user.email;
        this.newAuthCheck();
      }).catch((error) => {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });

  }

  redirect(){
    this.data.setTempAdmin(this.email);
    this.router.navigate(['/adminhome']);
  }

  newAuthCheck() {
    this.data.getEmail(this.email);
    this.redirect();
  }

  checkDBConnect() { //needs to moved to service
    if (!this.dbIsConnected) {
      this.msg = "Cannot authorize at this time, please try again later";
    }
  }

}
