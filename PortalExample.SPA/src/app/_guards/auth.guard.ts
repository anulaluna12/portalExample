import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlerifyService } from '../_services/alerify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor( private authServ: AuthService, private router: Router, private alertify: AlerifyService) {

  }
  canActivate(): boolean {
    if (this.authServ.loggedIn()) {
    return true;
    }
    this.alertify.error('Nie masz uprawień');
    this.router.navigate(['/home']);
    return false;
  }
}
