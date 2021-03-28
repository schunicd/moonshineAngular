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
  user: any;
  email: any;
  msg: string;
  authorizedEmails:string[] = new Array("schunicd@gmail.com", "preet.ghuman911@gmail.com", "mohammed.a.r.musleh@gmail.com", "felucas@sheridancollege.ca");
  isAdmin:boolean = false;
  dbIsConnected:boolean = true;


  constructor(){

  }

  ngOnInit(): void{
    var provider = new firebase.auth.GoogleAuthProvider(); //declaring provider
    this.provider = provider;

    this.checkDBConnect();
  
  }

  loginWithGmail(){ //function to connect to gmail
    firebase.auth().signInWithPopup(this.provider)
      .then((result) => {
        /** @type {firebase.auth.OAuthCredential} */
        var credential = result.credential;
        // This gives you a Google Access Token. You can use it to access the Google API.
        // let a: any;
        // a = result.credential
        // var token = a.accessToken;
        // The signed-in user info.
        this.user = result.user;
        this.email = result.user.email;
        this.authCheck();
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

  authCheck(){
    this.authorizedEmails.forEach(item => {
      console.log(item + " authChecked");
      if(this.user.email == item){
        this.isAdmin = true;
        this.msg = "You are an admin!";
        return;
      }
      else{
        this.msg = "You are not authorized!"
      }
    });
  }

  checkDBConnect(){
    if(!this.dbIsConnected){
      this.msg = "Cannot authorize at this time, please try again later";
    }
  }

}
