import { AuthService } from './../services/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/do';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService){ }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = this.authService.authToken;
    if(authToken) {
      request = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + authToken)
      });
    }

    return next.handle(request).pipe( catchError( err => {
      if( err.status === 401 ) {
        this.authService.logout();
        location.reload(true);
      }

      if( err.status === 404 ) {
        console.log("404 error occurred");
      }

      const error = err.error.message || err.statusText;
      return throwError(error);
    }))
  }

}
