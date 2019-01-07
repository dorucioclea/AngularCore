import { User } from './../models/user';
import { LoginResponse } from './../models/login-response';
import { RegisterForm } from '../models/register-form';
import { LoginForm } from './../models/login-form';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap, catchError, first } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';
import * as moment from "moment";
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = "/api/v1/auth";

  private authTokenFieldName  = 'auth_token';
  private expiresAtFieldName = 'expires_at';
  private uidFieldName = 'UID';

  public loggedUserSubject : BehaviorSubject<User>;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    this.loggedUserSubject = new BehaviorSubject<User>(undefined);
    if(!this.checkForSavedUser()) {
      this.logout();
    }
  }

  public get isLoggedIn() {
    return moment().isBefore(this.getExpiration())
  }

  public get isLoggedOut() { return !this.isLoggedIn }

  public get loggedUserValue() {
    return this.loggedUserSubject.getValue();
  }

  public get authToken() : string {
    return localStorage.getItem(this.authTokenFieldName);
  }

  public login(form: LoginForm) {
    return this.http.post<LoginResponse>(this.authUrl + "/login", form).pipe(
      first(),
      tap(
        data => this.setSession(data)
      ),
      catchError( (error : HttpErrorResponse) => {
        return this.handleError('login', error, form)
      })
    );
  }

  public register(form: RegisterForm) {
    return this.http.post<LoginResponse>(this.authUrl + "/register", form).pipe(
      first(),
      tap(
        data => this.setSession(data)
      ),
      catchError( (error) => {
        return this.handleError('register', error, form);
      })
    );
  }

  public async checkIfUserExists(userId?: string) {
    if( !userId && this.loggedUserValue ) {
      userId = this.loggedUserValue.id;
    }

    if( userId ){
      try {
        await this.http.get<User>("/api/v1/users/" + userId).toPromise();
        console.log("User with id: [" + userId + "] exists");
        this.renewSession()
        return true
      } catch(err) {
        console.log("User fetch error: " + err);
      }
    }
    this.logout();
    return false;
  }

  private async checkForSavedUser() {
    let userId = localStorage.getItem(this.uidFieldName);

    if(!userId) {
      return false;
    }

    try {
      await this.http.get<User>("/api/v1/users/" + userId).toPromise();
      console.log("User with id: [" + userId + "] exists");
      this.renewSession()
    } catch(err) {
      console.log("User fetch error: " + err);
      return false;
    }

    return true;
  }

  public logout() {
    this.clearSession();
    console.log("Logging out!");
    this.router.navigate(['/auth']);
  }

  private renewSession() {
    this.http.get<LoginResponse>(this.authUrl + "/renew").toPromise()
      .then( user => {
        this.setSession(user);
      }).catch( () => {
        this.logout();
      })
  }

  public renewOrClearSession() {
    this.http.get<LoginResponse>(this.authUrl + "/renew").toPromise()
      .then( user => {
        this.setSession(user);
      }).catch( () => {
        this.clearSession();
      })
  }

  private getExpiration() {
    const expiration = localStorage.getItem(this.expiresAtFieldName);
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  private setSession(authResult: LoginResponse) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');
    localStorage.setItem(this.authTokenFieldName, authResult.jwtToken);
    localStorage.setItem(this.uidFieldName, authResult.user.id);
    localStorage.setItem(this.expiresAtFieldName, JSON.stringify(expiresAt.valueOf()));
    this.loggedUserSubject.next(authResult.user);
  }

  public clearSession() {
    localStorage.removeItem(this.uidFieldName);
    localStorage.removeItem(this.authTokenFieldName);
    localStorage.removeItem(this.expiresAtFieldName);
    this.loggedUserSubject.next(undefined);
  }

  private handleError(where: string, why: HttpErrorResponse, what?: any) {
    let message = "Error occured while executing " + where.toUpperCase();
    if(what){
      message += "\n[REQUEST]: " + JSON.stringify(what);
    }
    message += "\n[RESPONSE]: " + JSON.stringify(why);
    console.warn(message);
    return _throw(why);
  }
}
