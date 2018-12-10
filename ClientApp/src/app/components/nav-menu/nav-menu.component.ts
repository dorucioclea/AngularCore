import { AuthService } from './../../services/auth.service';
import { Component } from '@angular/core';
import { LoggedUser } from '../../models/logged-user';
import { ObservableMedia, MediaChange } from '@angular/flex-layout';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  opened = true;
  over = 'side';
  expandHeight = '42px';
  collapseHeight = '42px';
  displayMode = 'flat';

  watcher: Subscription;

  constructor(
    public authService: AuthService,
    private media: ObservableMedia
  ) {
    this.watcher = media.subscribe((change: MediaChange) => {
      if (change.mqAlias === 'sm' || change.mqAlias === 'xs') {
        this.opened = false;
        this.over = 'over';
      } else {
        this.opened = true;
        this.over = 'side';
      }
    });
  }

  public get loggedUser(): LoggedUser {
    return this.authService.loggedUser;
  }

  public toggleSideNav() {
    this.opened = !this.opened;
  }

}
