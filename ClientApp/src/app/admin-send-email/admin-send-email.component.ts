import { Component, OnInit } from '@angular/core';
import { StringLiteralLike } from 'typescript';
import { DataService } from '../data.service';


@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css']
})
export class AdminSendEmailComponent implements OnInit {

  constructor(private data: DataService) { }

  emailSubject: String;
  emailBody: String;
  emailImage: File;

  completeEmail: Email;

  ngOnInit() {
  }

  onFileSelected(event){
    this.emailImage = event.target.files[0];
  }

  sendEmail(){
    console.log(this.emailSubject);
    console.log(this.emailBody);
    console.log(this.emailImage);

    let email = {
      emailSubject: this.emailSubject,
      emailBody: this.emailBody,
      emailImage: this.emailImage
    }

    //this.data.sendEmail(email);
  }

}

interface Email {
  emailSubject: String;
  emailBody: String;
  emailImage: String;
}
