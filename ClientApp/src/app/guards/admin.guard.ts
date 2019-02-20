import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from '@app/services/auth.service';
import { map, catchError, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService,
    private http: HttpClient
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.loggedUserValue === undefined) {
      return of(false);
    }

    return this.authService.checkIfAdmin(this.authService.loggedUserValue.id)
      .pipe(
        tap( response => this.authService.isAdmin = true),
        map( response => true ),
        catchError(error => {
          this.authService.isAdmin = false;
          return of(false);
        })
      );
  }
}
