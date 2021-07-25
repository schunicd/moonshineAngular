import { Component, OnInit } from '@angular/core';
import { CanActivate, Router, ActivatedRoute } from '@angular/router';
import firebase from "firebase/app";
import "firebase/auth";

@Component({
  selector: 'app-adminhome',
  templateUrl: './adminhome.component.html',
  styleUrls: ['./adminhome.component.css']
})
export class AdminhomeComponent implements OnInit, CanActivate {

  constructor(private router: Router, private route: ActivatedRoute) { }

  authorize: any;

  ngOnInit() {
    this.authorize = this.route.snapshot.paramMap.get('authCheck');
    this.canActivate()
  }

  canActivate(){
    if(this.authorize != 'true'){
      this.router.navigate(['']);
      return false;
    }
    return true;
  }

}
