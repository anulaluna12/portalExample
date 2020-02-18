import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlerifyService } from '../_services/alerify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlerifyService, private router: Router) { }

  ngOnInit() { }
  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('Zalogowałeś sie do aplikajci');
      },
      error => {
        this.alertify.error('Wystąpił błąd logowania');
      },
      () => {
        this.router.navigate(['/users']);
      }
    );
  }
  loggedIn() {
    return this.authService.loggedIn();
  }
  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Zostałeś wylogowany');
    this.router.navigate(['/home']);
  }
}
