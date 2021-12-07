import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { reverse } from 'dns';

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
  displayAlbum: string;
  buttonImage: string;
  albumPhotos: string[] = [];
  albums: string[] = [];

  getAlbumPhotos(album: string){
      this.albumPhotos = [];
      this.displayAlbum = album;
      this.photos.forEach(photo => {
        if(photo.includes(album + "/") && photo.split(album + "/")[1] != "")
        {
          this.albumPhotos.indexOf(album)
          this.albumPhotos.push(photo);
        }
      });
  }

  getButtonImage(album: string){
    this.photos.forEach(ap => {
      // if(ap.includes(album))
      // {
      //   this.buttonImage = ap;
      // }
      if(ap.split("/")[1] == album){
        console.log(ap.split("/")[0])
        this.buttonImage = ap
      }
    });
    return this.buttonImage;
  }

  backToGallery(){
    this.albumPhotos = [];
  }

  async ngOnInit() {
    this.http.get<any[]>(this.baseUrl + 'api/imageUpload/GetPhotos').subscribe(result => {
      result.forEach(res => {
        if(res.includes("photoAlbums/") && res.split("/")[1] != "" && res.split("/")[2] != "")
        {
          this.photos.push(res);
        }
      });

      console.log(result);
      console.log(this.photos);
      this.photos.forEach(photo => {
        if(photo.includes("photoAlbums/"))
        {
          if(photo.split("/")[1] != "" && !this.albums.includes(photo.split("/")[1]))
          {
            this.albums.push(photo.split("/")[1]);
            console.log(this.albums);
          }
        }
      });
    }, error => console.error(error));
  }

}
