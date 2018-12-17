import { FriendService } from './../../services/friend.service';
import { AuthService } from './../../services/auth.service';
import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { LoggedUser } from '../../models/logged-user';
import { ObservableMedia, MediaChange } from '@angular/flex-layout';
import { Subscription, Observable } from 'rxjs';
import { MatSidenav } from '@angular/material';
import { UserService } from '@app/services/user.service';
import { User } from '@app/models/user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit, OnDestroy {

  @ViewChild('sidenav') sidenav : MatSidenav;

  opened = true;
  manuallyToggled = false;
  over = 'side';
  expandHeight = '42px';
  collapseHeight = '42px';
  displayMode = 'flat';

  subscriptions: Subscription;
  userFriends: User[];

  constructor(
    public authService: AuthService,
    private friendService: FriendService,
    private media: ObservableMedia
  ) {
    this.subscriptions = media.subscribe((change: MediaChange) => {
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
    this.subscriptions.add(this.friendService.friendList$.subscribe( (list) => {
      this.userFriends = list;
    }))
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
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
