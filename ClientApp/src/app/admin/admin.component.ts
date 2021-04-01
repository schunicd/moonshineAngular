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
  email: string;
  msg: string;
  authorizedEmails:string[] = new Array("mohammed.a.r.musleh@gmail.com", "schunicd@gmail.com", "preet.ghuman911@gmail.com", "felucas@sheridancollege.ca");
  isAdmin:boolean = false;
  dbIsConnected:boolean = true;


  constructor(){

  }

  ngOnInit(): void{
    var provider = new firebase.auth.GoogleAuthProvider(); //declaring provider
    this.provider = provider;

    this.checkDBConnect();

  }

  loginWithGmail(){
    firebase.auth().signInWithPopup(this.provider)
      .then((result) => {
        /** @type {firebase.auth.OAuthCredential} */
        var credential = result.credential;

        this.user = result.user;
        this.email = result.user.email;
        this.authCheck();
      }).catch((error) => {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });

  }

  authCheck(){
<<<<<<< HEAD
    this.msg = "You are not authorized!";
    this.authorizedEmails.forEach(item => {
      console.log(this.user.email);
      console.log(item + " authChecked");
=======
    this.msg = "You are not authorized!"
    this.authorizedEmails.forEach(item => {
>>>>>>> c0124e81a2a22273249c820448af964cdbe8aac5
      if(this.user.email == item){
        this.isAdmin = true;
        this.msg = "You are an admin!";
        return;
      }
<<<<<<< HEAD

=======
>>>>>>> c0124e81a2a22273249c820448af964cdbe8aac5
    });
    return;
  }

  checkDBConnect(){
    if(!this.dbIsConnected){
      this.msg = "Cannot authorize at this time, please try again later";
    }
  }

}
