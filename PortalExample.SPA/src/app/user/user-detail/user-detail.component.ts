import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';
import { AlerifyService } from 'src/app/_services/alerify.service';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: User;
  constructor(
    private userSer: UserService,
    private alertify: AlerifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
   this. loadUser();

  }
  loadUser() {
    this.userSer.getUser(this.route.snapshot.params.id).subscribe(
      (user: User) => {
        this.user = user;
      }, error => {
        this.alertify.error(error);
      }
    );
  }
}