import { Injectable } from '@angular/core';
import { Resolve, Route, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import { AlerifyService } from '../_services/alerify.service';
import { Observable, of } from 'rxjs';
import { error } from 'protractor';
import { catchError } from 'rxjs/operators';


@Injectable()
export class UserListResolver implements Resolve<User[]> {
    constructor(private userSer: UserService, private router: Router, private alerify: AlerifyService) {

    }
    resolve(route: ActivatedRouteSnapshot): Observable<User[]>  {
        return this.userSer.getUsers()
        .pipe(
            catchError(error => {
                this.alerify.error('Problem z pobraniem danych');
                this.router.navigate(['/home']);
                return of(null);
            })
        )
        ;
    }
}
