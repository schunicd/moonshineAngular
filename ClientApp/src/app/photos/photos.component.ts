import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { stringify } from 'querystring';

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})

export class PhotosComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    /*
    const galleryFolder = '../../assets/PhotoGallery';
    const fs = require('fs');

    fs.readdir(galleryFolder, (err, files) => {
      console.log("PRINTING FILE PATHS");
      files.array.forEach(file => {
        console.log(file);
      });
    });
    */

  }

  catcher: any[];
  photos: string[] = [];


  async ngOnInit() {
    await this.http.get<any[]>(this.baseUrl + 'api/imageUpload').subscribe(result => {
      this.catcher = result;
      this.setPhotos(result);
      console.log(result);
    }, error => console.error(error));
  }

  setPhotos(newArray: any[]) {
    var temp: string = ""
    newArray.forEach(path => {
      this.photos.push("..\\" + path.trim())
    });
  }

}
