import { Component, OnInit } from '@angular/core';
import firebase from "firebase/app";
import "firebase/auth";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  provider: any;

  constructor(){

  }

  ngOnInit(): void{
    var provider = new firebase.auth.GoogleAuthProvider(); //declaring provider
    this.provider = provider;
  }

  loginWithGmail(){ //function to connect to gmail
    console.log("Gmail");
    firebase.auth().signInWithPopup(this.provider)
      .then((result) => {
        /** @type {firebase.auth.OAuthCredential} */
        var credential = result.credential;

        // This gives you a Google Access Token. You can use it to access the Google API.
        // let a: any;
        // a = result.credential
        // var token = a.accessToken;
        // The signed-in user info.
        var user = result.user;
        console.log(user);
        // ...
      }).catch((error) => {
    // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // The email of the user's account used.
        var email = error.email;
        // The firebase.auth.AuthCredential type that was used.
        var credential = error.credential;
        // ...
      }); 
  }

}
