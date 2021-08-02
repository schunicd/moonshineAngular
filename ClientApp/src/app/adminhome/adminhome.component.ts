import { Component, OnInit, Inject } from '@angular/core';
import { CanActivate, Router, ActivatedRoute } from '@angular/router';
import { DataService } from '../data.service';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import firebase from "firebase/app";
import { Admin } from '../Admin';
import "firebase/auth";

@Component({
  selector: 'app-adminhome',
  templateUrl: './adminhome.component.html',
  styleUrls: ['./adminhome.component.css']
})
export class AdminhomeComponent implements OnInit, CanActivate {

  constructor(private router: Router, private route: ActivatedRoute, private data: DataService) { }

  adminToCheck: string;
  subscription: Subscription;
  private isAdmin: boolean = false;

  ngOnInit() {
    this.subscription = this.data.currentCheck.subscribe(tempAdminCheck => this.adminToCheck = tempAdminCheck);
    this.adminInDataBase();
    this.canActivate()
  }

  canActivate(){
    if(!this.isAdmin){
      this.router.navigate(['']);
      return false;
    }
    return true;
  }

  adminInDataBase(){
    if(this.data.getEmail(this.adminToCheck)){
      this.isAdmin = true;
    }
    else{
      this.isAdmin = false;
      console.log("I'm redirecting you because I suck at async functions")
    }
  }

}
