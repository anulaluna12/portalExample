import { AlerifyService } from "./../_services/alerify.service";
import { AuthService } from "./../_services/auth.service";
import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { FormGroup, FormControl } from "@angular/forms";
@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  constructor(
    private authService: AuthService,
    private alertify: AlerifyService
  ) {}

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl(),
      password: new FormControl(),
      confirmPassword: new FormControl()
    });
  }
  register() {
    // this.authService.register(this.model).subscribe(
    //   () => {
    //     this.alertify.success("Rejestracja udana");
    //   },
    //   error => {
    //     this.alertify.error(error);
    //   }
    // );
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
