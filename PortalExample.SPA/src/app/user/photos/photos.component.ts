import { AuthService } from "src/app/_services/auth.service";
import { environment } from "src/environments/environment";
import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Photo } from "../../_models/Photo";
import { FileUploader } from "ng2-file-upload";
import { UserService } from "src/app/_services/user.service";
import { AlerifyService } from "src/app/_services/alerify.service";
import { error } from 'protractor';

@Component({
  selector: "app-photos",
  templateUrl: "./photos.component.html",
  styleUrls: ["./photos.component.css"]
})
export class PhotosComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getuserPhotoChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = environment.apiUrl;
  currentMain: Photo;
  constructor(
    private authSer: AuthService,
    private userService: UserService,
    private alertify: AlerifyService
  ) {}
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  ngOnInit() {
    this.initializeUploader();
  }
  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl + "user/" + this.authSer.decodedToken.nameid + "/photos",
      authToken: "Bearer " + localStorage.getItem("token"),
      isHTML5: true,
      allowedFileType: ["image"],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo: Photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    };
  }
  setMainPhoto(photo: Photo) {
    this.userService
      .setMainPhoto(this.authSer.decodedToken.nameid, photo.id)
      .subscribe(
        next => {
          console.log("Sukces, zdjęcie ustawione jako głowne");
          this.currentMain = this.photos.filter(p => p.isMain === true)[0];
          this.currentMain.isMain = false;
          photo.isMain = true;
          // this.getuserPhotoChange.emit(photo.url);
          this.authSer.changeUserPhoto(photo.url);
          this.authSer.currentUser.photoUrl = photo.url;
          localStorage.setItem(
            "user",
            JSON.stringify(this.authSer.currentUser)
          );
        },
        error => {
          this.alertify.error(error);
        }
      );
  }
  deletePhoto(id: number) {
    this.alertify.confirm("Czy jesteś pewien że cchesz usunąc zdjęcie", () => {
      this.userService
        .deletePhoto(this.authSer.decodedToken.nameid, id)
        .subscribe(() => {
          this.photos.splice(
            this.photos.findIndex(p => p.id === id),
            1
          );
          this.alertify.success('Zdjecie zostało usunięte');
        },
        error => {
          this.alertify.error('Nie udało się usunąć  xdjęcia ');
        });
    });
  }
}
