import { User } from './../models/user';
import { LoginResponse } from './../models/login-response';
import { RegisterForm } from '../models/register-form';
import { LoggedUser } from './../models/logged-user';
import { LoginForm } from './../models/login-form';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap, catchError, first } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';
import { Subject } from 'rxjs/Subject';
import * as moment from "moment";
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = "/api/Auth";
  private authTokenFieldName  = 'auth_token';
  private expiresAtFieldName = 'expires_at';
  private loggedUsedFieldName = 'user';

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    if(!this.checkIfUserExists()) {
      this.logout();
    }
  }

  public get isLoggedIn()  { return moment().isBefore(this.getExpiration()) }
  public get isLoggedOut() { return !this.isLoggedIn }

  public get loggedUser() : LoggedUser {
    return JSON.parse(localStorage.getItem(this.loggedUsedFieldName));
  }

  public get authToken() : string {
    return localStorage.getItem(this.authTokenFieldName);
  }

  public login(form: LoginForm) {
    return this.http.post<LoginResponse>(this.authUrl + "/Login", form).pipe(
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
    return this.http.post<LoginResponse>(this.authUrl + "/Register", form).pipe(
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
    if( !userId && this.loggedUser ) {
      userId = this.loggedUser.id;
    }

    if( userId ){
      try {
        await this.http.get<User>("/api/User/GetUser/" + userId).toPromise();
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

  public logout() {
    this.clearSession();
    console.log("Logging out!");
    this.router.navigate(['/auth']);
  }

  private renewSession() {
    this.http.get<LoginResponse>(this.authUrl + "/RenewSession/").toPromise()
      .then( user => {
        this.setSession(user);
      }).catch( () => {
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
  }

  public clearSession() {
    localStorage.removeItem(this.loggedUsedFieldName);
    localStorage.removeItem(this.authTokenFieldName);
    localStorage.removeItem(this.expiresAtFieldName);
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
