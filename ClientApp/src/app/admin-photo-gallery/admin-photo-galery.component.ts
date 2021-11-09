

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-admin-photo-galery',
  templateUrl: './admin-photo-galery.component.html',
  styleUrls: ['./admin-photo-galery.component.css']
})
export class AdminPhotoGaleryComponent implements OnInit {

  public progress: number;
  public message: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  upload(files) {
    if (files.length === 0)
      return;

    var x = (<HTMLInputElement>document.getElementById("uploadPhoto")).value;

    console.log(x);

    const formData = new FormData();
    for (let file of files)
      formData.append(file.name, file);
    const uploadReq = new HttpRequest('POST', `api/imageUpload/AddFile`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {

        this.message = "Photo Uploaded Successfully!";

    });

  }

  delete(files){
    if(files.length === 0)
      return;
    const formData = new FormData();
    for(let file of files)
      formData.append(file.name, file);
    const deleteReq = new HttpRequest('DELETE', `api/imageUpload/`, formData);
    this.http.request(deleteReq).subscribe(event => {
      console.log(event);
    })
  }


  ngOnInit() {
  }



}
