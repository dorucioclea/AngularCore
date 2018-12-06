import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router){ }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = localStorage.getItem('auth_token');
    if(authToken) {
      request = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + authToken)
      });
    }

    return next.handle(request).do( (event: HttpEvent<any>) => {},
      (err: any) => {
        if( err instanceof HttpErrorResponse && err.status === 401) {
          this.router.navigate(['/auth']);
        }
      })
  }

}
