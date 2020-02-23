import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';
import { AlerifyService } from 'src/app/_services/alerify.service';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  user: User;
  constructor(
    private userSer: UserService,
    private alertify: AlerifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
    this.galleryOptions = [
      {
          width: '600px',
          height: '400px',
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide
      }
  ];

    this.galleryImages = this.getImages();
  }
  getImages(){
    const imagesUrls=[];
    for (let i = 0; i < this.user.photos.length; i++) {
      imagesUrls.push({
          small:this.user.photos[i].url,
          medium:this.user.photos[i].url,
          big:this.user.photos[i].url,
          description:this.user.photos[i].description
      });
   return imagesUrls;
      
    }
  }
  // loadUser() {
  //   this.userSer.getUser(this.route.snapshot.params.id).subscribe(
  //     (user: User) => {
  //       this.user = user;
  //     }, error => {
  //       this.alertify.error(error);
  //     }
  //   );
  // }
}
