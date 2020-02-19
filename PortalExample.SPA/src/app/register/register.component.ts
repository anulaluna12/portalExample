import { AlerifyService } from './../_services/alerify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private authService: AuthService, private alertify: AlerifyService) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Rejestracja udana');
    }, error => {
     this. alertify.error(error);
    }
    );
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
