import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
  tempAdmin: any;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){

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
        this.newAuthCheck();
      }).catch((error) => {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });
      
  }

  authCheck(){
    this.msg = "You are not authorized!"
    this.authorizedEmails.forEach(item => {
      if(this.user.email == item){
        this.isAdmin = true;
        this.msg = "You are an admin!";
        return;
      }
    });
  }

  newAuthCheck(){
    this.http.get<Admin[]>(this.baseUrl + 'api/Admins/email='+this.email).subscribe(result => {
      this.tempAdmin = result;
      console.log(this.tempAdmin);
    }, error => console.error(error));
  }

    checkDBConnect(){
      if(!this.dbIsConnected){
        this.msg = "Cannot authorize at this time, please try again later";
      }
    }
  
  }
  
  interface Admin {
    ID: number,
    name: string,
    email: string,
    phoneNumber: string,
    accessLevel: number
  }