import { LoggedUser } from './../models/logged-user';
import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService
  ){ }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if( this.authService.checkIfUserExists() ) {
      return true;
    }

    this.router.navigate(['/auth'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
