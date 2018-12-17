import { AuthService } from './../../services/auth.service';
import { Component, ViewChild, OnInit } from '@angular/core';
import { LoggedUser } from '../../models/logged-user';
import { ObservableMedia, MediaChange } from '@angular/flex-layout';
import { Subscription, Observable } from 'rxjs';
import { MatSidenav } from '@angular/material';
import { UserService } from '@app/services/user.service';
import { FriendUser } from '@app/models/friend-user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  @ViewChild('sidenav') sidenav : MatSidenav;

  opened = true;
  manuallyToggled = false;
  over = 'side';
  expandHeight = '42px';
  collapseHeight = '42px';
  displayMode = 'flat';

  watcher: Subscription;
  userFriends$: Observable<FriendUser[]>;

  constructor(
    public authService: AuthService,
    private userService: UserService,
    private media: ObservableMedia
  ) {
    this.watcher = media.subscribe((change: MediaChange) => {
      if (change.mqAlias === 'sm' || change.mqAlias === 'xs') {
        this.opened = false;
        this.over = 'over';
      } else {
        if(!this.manuallyToggled){
          this.opened = true;
        }
        this.over = 'side';
      }
    });
  }

  ngOnInit() {
    this.userFriends$ = this.userService.getUserFriends(this.loggedUser.id);
  }

  public get loggedUser(): LoggedUser {
    return this.authService.loggedUser;
  }

  public toggleSideNav() {
    if( this.over != 'over' ){
      this.manuallyToggled = !this.manuallyToggled;
    }
    this.opened = !this.opened;
  }

  public linkClick() {
    if(this.over === 'over') {
      this.sidenav.close();
    }
  }

}
