import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { stringify } from 'querystring';

const path = "../assets/images"

@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})

export class PhotosComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { 
  }
  
  catcher: any[];
  photos: string[] = [];


  async ngOnInit() {
    await this.http.get<any[]>(this.baseUrl + 'api/imageUpload').subscribe(result => {
      this.catcher = result;
      this.setPhotos(result);
    }, error => console.error(error));
  }

  setPhotos(newArray: any[]) {
    var temp: string = ""
    newArray.forEach(path => {
      this.photos.push("..\\" + path.trim())
    });
  }

}
