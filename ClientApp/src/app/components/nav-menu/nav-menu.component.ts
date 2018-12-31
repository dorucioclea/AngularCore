import { AuthService } from './../../services/auth.service';
import { Component, ViewChild, OnInit} from '@angular/core';
import { ObservableMedia, MediaChange } from '@angular/flex-layout';
import { Subscription, Observable } from 'rxjs';
import { MatSidenav } from '@angular/material';
import { User } from '@app/models/user';

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
  loggedUser$: Observable<User>;

  constructor(
    public authService: AuthService,
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
    this.loggedUser$ = this.authService.loggedUserSubject;
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
