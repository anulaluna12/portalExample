import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/User';
import { ActivatedRoute } from '@angular/router';
import { AlerifyService } from 'src/app/_services/alerify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  user: User;
  constructor(
    private route: ActivatedRoute,
    private alertify: AlerifyService
  ) {}
  @ViewChild('editForm') editForm: NgForm;
  @HostListener('window:beforeunload', ['$enent'])
  unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }
  updateUser() {
    console.log(this.user);
    this.alertify.success('Profil po,yśłe zaktualizowany');
    this.editForm.reset(this.user);
  }

}
