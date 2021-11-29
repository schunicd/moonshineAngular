import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
@Component({
  selector: 'app-admin-photo-galery',
  templateUrl: './admin-photo-galery.component.html',
  styleUrls: ['./admin-photo-galery.component.css']
})

export class AdminPhotoGaleryComponent implements OnInit {

  public progress: number;
  public message: string;
  public albumName: string;
  public fileName: string = "";
  photos: string[] = [];
  albums: string[] = [];
  albumPhotos: string[] = [];
  newFiles: string[] = [];

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

    this.photos.push("photoAlbums/" + this.albumName + "/" + this.fileName);

  }

  lockAlbumName(){
    if(this.albumName == "Create New Album"){
      (<HTMLInputElement> document.getElementById('newAlbum')).disabled = false;
    }
    else{
      this.getAlbumPhotos(this.albumName);
      (<HTMLInputElement> document.getElementById('newAlbum')).disabled = true;
    }
  }

  deleteFromAlbum(photo: string, albumName: String){
    console.log(photo.split("/")[1]);
    console.log(albumName);

    const deleteReq = new HttpRequest('DELETE', 'api/imageUpload/DeletePhoto/' + albumName + "/" + photo.split("/")[2]);
    this.http.request(deleteReq).subscribe(event => {
      this.message = "Photo Deleted";
    });
    this.photos = this.photos.filter(p => p != photo);
    this.albumPhotos = this.albumPhotos.filter(p => p != photo);
  }

  getAlbumPhotos(album: string){
    if(album == null){
      return;
    }
    this.albumPhotos = [];
    this.photos.forEach(photo => {
      if(photo.includes(album + "/") && photo.split(album + "/")[1] != "")
      {
        this.albumPhotos.indexOf(album)
        this.albumPhotos.push(photo);
      }
    });
}

  deleteFromGallery(file, albumName){
    //Call api with choice 1
    const bucketChoice = 1;

    const deleteReq = new HttpRequest('DELETE', 'api/imageUpload/DeletePhoto/' + albumName + "/" + this.fileName
            +"/" + bucketChoice);

    this.http.request(deleteReq).subscribe(event =>{
      this.message = "Photo Deleted";
    });
  }

  deleteAlbum(albumName){
    const bucketChoice = 1;

    const deleteReq = new HttpRequest('DELETE', 'api/imageUpload/DeleteFolder/' + albumName +"/" + bucketChoice);

    this.http.request(deleteReq).subscribe(event =>{
      this.message = "Folder Deleted";
    });

    console.log(albumName)
  }

  ngOnInit() {
    this.getPhotos();
  }

  updatePhotoArray(){

    this.albumPhotos.push("photoAlbums/" + this.albumName + "/" + this.fileName);
  }

  getPhotos(){
    this.http.get<any[]>(this.baseUrl + 'api/imageUpload/GetPhotos').subscribe(result => {
      result.forEach(res => {
        if(res.includes("photoAlbums/") && res.split("/")[1] != "" && res.split("/")[2] != "")
        {
          this.photos.push(res);
        }
      }, error => console.log(error));


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
