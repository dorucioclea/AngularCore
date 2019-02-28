import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable, Injector } from '@angular/core';
import 'rxjs/add/operator/do';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  /* TODO: inject AuthService and provide token and session operations from it
      - as for before it was initialized too late and interceptor had undefined service causing error */
  constructor(){ }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = localStorage.getItem('auth_token');
    if(authToken) {
      request = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + authToken)
      });
    }

    return next.handle(request).pipe( catchError( err => {
      if( err.status === 401 ) {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('user');
        localStorage.removeItem('expires_at')
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
