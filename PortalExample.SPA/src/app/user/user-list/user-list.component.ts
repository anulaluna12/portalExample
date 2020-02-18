import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/User';
import { AlerifyService } from 'src/app/_services/alerify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  constructor(
    private userService: UserService,
    private alerify: AlerifyService
  ) {}

  ngOnInit() {
    this.loadUsers() ;

  }
  loadUsers() {
    this.userService.getUsers().subscribe(
      users => {
        this.users = users;
      },
      error => {
        this.alerify.error(error);
      }
    );
  }
}
