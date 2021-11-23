

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
  public albumName: string;
  public fileName: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  upload(files, albumName) {
    if (files.length === 0)
      return;

    const formData = new FormData();
    for (let file of files){
      formData.append(file.name, file);
      this.fileName = file.name;
    }
    const uploadReq = new HttpRequest('POST', `api/imageUpload/AddFile/` + albumName, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {

        this.message = "Photo Uploaded Successfully!";

    });

  }

  deleteFromGallery(file, albumName){
    //Call api with choice 1
    const bucketChoice = 1;
    var toDelete = albumName +"/"+ this.fileName;
    console.log(toDelete);
    // const formData = new FormData();
    // formData.append(file[0].name, file[0])
    const deleteReq = new HttpRequest('DELETE', 'api/imageUpload/DeletePhoto/' + this.fileName);

    this.http.request(deleteReq).subscribe(event =>{
      this.message = "Photo Deleted";
    });
  }

  // delete(files){
  //   if(files.length === 0)
  //     return;
  //   const formData = new FormData();
  //   for(let file of files)
  //     formData.append(file.name, file);
  //   const deleteReq = new HttpRequest('DELETE', `api/imageUpload/`, formData);
  //   this.http.request(deleteReq).subscribe(event => {
  //     console.log(event);
  //   })
  // }


  ngOnInit() {
  }



}
