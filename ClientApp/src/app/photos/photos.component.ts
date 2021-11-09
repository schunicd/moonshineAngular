import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { stringify } from 'querystring';

const BUCKET_PATH = "https://moonshinephotostest.s3.amazonaws.com/";

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})

export class PhotosComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  photos: string[] = [];

  async ngOnInit() {
    this.http.get<any[]>(this.baseUrl + 'api/imageUpload/GetPhotos').subscribe(result => {
      this.photos = result;
      console.log(this.photos);
    }, error => console.error(error));
  }

}
