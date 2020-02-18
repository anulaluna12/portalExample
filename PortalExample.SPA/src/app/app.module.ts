import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';

import { BsDropdownModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlerifyService } from './_services/alerify.service';
import { UserService } from './_services/user.service';
import { UserListComponent } from './user/user-list/user-list.component';

import { appRoutes } from './routes.routing';
import { AuthGuard } from './_guards/auth.guard';


export function tokenGetter3(){
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      UserListComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter : tokenGetter3,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }
      ),
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot()
   ],
   providers: [
      AuthService,
      AlerifyService,
      UserService,
      AuthGuard
      
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
