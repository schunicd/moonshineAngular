import { Component, OnInit, Inject } from '@angular/core';
import { DataService } from '../data.service';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css']
})
export class AdminSendEmailComponent implements OnInit {

  constructor(private _snackBar: MatSnackBar, private data: DataService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  emailSubject: String;
  emailBody: String;
  emailImage: File;

  ngOnInit() {
  }

  onFileSelected(event){
    console.log(event.target.files[0]);
    //this.emailImage = event.target.files[0];
  }

  sendEmail(files){
    console.log(this.emailSubject);
    console.log(this.emailBody);
    console.log(files[0].name);

    let email = {
      subject: this.emailSubject,
      body: this.emailBody,
      image: files[0].name
    }

    this.data.sendEmail(email);

    this.emailSubject = "";
    this.emailBody = "";
    this.emailImage = null;
    this.sentEmailSnackBar();
  }

  sentEmailSnackBar() {
    this._snackBar.open("Email Sent!", "Close", {duration: 5000});
  }

  uploadEmailImage(files){
    const formData = new FormData();
    for (let file of files){
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', `api/imageUpload/EmailImage/`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {

    });
  }

}

interface Email {
  subject: String;
  body: String;
  image: String;
}
