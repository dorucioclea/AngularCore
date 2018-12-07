import { AuthService } from './../../services/auth.service';
import { Component } from '@angular/core';
import { LoggedUser } from '../../models/logged-user';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  user$ : Observable<LoggedUser>;

  constructor(public authService: AuthService){
    this.user$ = authService.userSubject.asObservable();
  }

  public get loggedUser(): LoggedUser {
    return this.authService.loggedUser;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
