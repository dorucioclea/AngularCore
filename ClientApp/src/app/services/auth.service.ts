import { SpinnerOverlayService } from './spinner-overlay.service';
import { User } from './../models/user';
import { LoginResponse } from './../models/login-response';
import { RegisterForm } from '../models/register-form';
import { LoggedUser } from './../models/logged-user';
import { LoginForm } from './../models/login-form';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, tap, catchError } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { Subject } from 'rxjs/Subject';
import * as moment from "moment";
import { Router } from '@angular/router';

@Injectable()
export class AuthService {

  private authUrl = "/api/Auth";
  private authTokenFieldName  = 'auth_token';
  private expiresAtFieldName = 'expires_at';
  private loggedUsedFieldName = 'user';

  public userSubject : Subject<LoggedUser> = new Subject();

  constructor(private http: HttpClient,
              private spinnerService: SpinnerOverlayService,
              private router: Router) {
    let loggedUser = this.getLoggedUser();
    if( this.isLoggedIn && loggedUser ) {
      this.checkIfUserExists(loggedUser);
    } else {
      this.logout();
    }
  }

  login(form: LoginForm) {
    return this.http.post<LoginResponse>(this.authUrl + "/Login", form).pipe(
      tap(
        data => this.setSession(data)
      ),
      catchError( (error) => {
        return this.handleError('login', error, form)
      })
    );
  }

  register(form: RegisterForm) {
    return this.http.post<LoginResponse>(this.authUrl + "/Register", form).pipe(
      tap(
        data => this.setSession(data)
      ),
      catchError( (error) => {
        return this.handleError('register', error, form);
      })
    );
  }

  checkIfUserExists(loggedUser: LoggedUser) {
    this.spinnerService.show("Loading user...");
    console.log("Starting user check");
    this.http.get<User>("/api/User/GetUser/" + loggedUser.id).toPromise()
    .then( user => {
      console.log("User with id: [" + loggedUser.id + "] exists");
      this.renewSession()
    }).catch( err => {
      console.log("User fetch error: " + err);
      this.logout();
    });
    this.spinnerService.hide();
  }

  logout() {
    localStorage.removeItem(this.loggedUsedFieldName);
    localStorage.removeItem(this.authTokenFieldName);
    localStorage.removeItem(this.expiresAtFieldName);
    this.userSubject.next(undefined);
  }

  redirectToLogin() {
    this.router.navigate(['/auth']);
  }

  isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getLoggedUser() : LoggedUser {
    let user = JSON.parse(localStorage.getItem(this.loggedUsedFieldName));
    return user;
  }

  getAuthToken() : string {
    let token = localStorage.getItem(this.authTokenFieldName);
    return token;
  }

  private renewSession() {
    this.http.get<LoginResponse>(this.authUrl + "/RenewSession/").toPromise()
      .then( user => {
        this.setSession(user);
      }).catch( err => {
        this.logout();
      })
  }

  private getExpiration() {
    const expiration = localStorage.getItem(this.expiresAtFieldName);
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  private setSession(authResult: LoginResponse) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');
    localStorage.setItem(this.loggedUsedFieldName, JSON.stringify(authResult.user));
    localStorage.setItem(this.authTokenFieldName, authResult.jwtToken);
    localStorage.setItem(this.expiresAtFieldName, JSON.stringify(expiresAt.valueOf()));
    this.userSubject.next(authResult.user);
  }

  private handleError(where: string, why: object, what?: object) : ErrorObservable {
    let message = "Error occured while executing " + where.toUpperCase();
    if(what){
      message += "\n[REQUEST]: " + JSON.stringify(what);
    }
    message += "\n[RESPONSE]: " + JSON.stringify(why);
    return _throw(message);
  }
}
