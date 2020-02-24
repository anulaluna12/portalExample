import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwthelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  photoUrl = new BehaviorSubject<string>('../../assets/zd.png');
currenthotoUrl = this.photoUrl.asObservable();
constructor(private http: HttpClient) {

}
changeUserPhoto(photoUrl:string){
this.photoUrl.next(photoUrl);
}
login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((respone: any) => {
        const user = respone;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', user.user);
          this.decodedToken = this.jwthelper.decodeToken(user.token);
          this.currentUser = user.user;
          console.log(this.decodedToken);
          this.changeUserPhoto(this.currentUser.photoUrl);
        }
      })
    );
  }

register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
loggedIn():boolean {
    const token = localStorage.getItem('token');
    return !this.jwthelper.isTokenExpired(token);
  }
}
