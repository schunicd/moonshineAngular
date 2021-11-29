import { Component, OnInit, Inject } from '@angular/core';
import { DataService } from '../data.service';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css']
})
export class AdminSendEmailComponent implements OnInit {

  constructor(private data: DataService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

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

    const formData = new FormData();
    for (let file of files){
      formData.append(file.name, file);
    }

    let email = {
      subject: this.emailSubject,
      body: this.emailBody,
      image: ""
    }

    const uploadReq = new HttpRequest('POST', `api/AdminSendEmail/` + email, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
        console.log(event);
    });

    //this.data.sendEmail(email);
  }

}

interface Email {
  subject: String;
  body: String;
  image: String;
}
