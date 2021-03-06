import { FriendService } from './../../services/friend.service';
import { AuthService } from './../../services/auth.service';
import { Component, ViewChild, OnInit, OnDestroy} from '@angular/core';
import { ObservableMedia, MediaChange } from '@angular/flex-layout';
import { Subscription, Observable } from 'rxjs';
import { MatSidenav } from '@angular/material';
import { User } from '@app/models/user';
import { ImageService } from '../../services/image.service';
import { Image } from '../../models/image';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit, OnDestroy{

  @ViewChild('sidenav') sidenav : MatSidenav;

  opened = true;
  manuallyToggled = false;
  over = 'side';
  expandHeight = '42px';
  collapseHeight = '42px';
  displayMode = 'flat';

  loggedUser$: Observable<User>;
  friendList$: Observable<User[]>;

  public profilePictureUrl: string;

  subscription: Subscription = new Subscription();

  constructor(
    public authService: AuthService,
    private imageService: ImageService,
    private media: ObservableMedia,
    private friendService: FriendService
  ) {
    this.subscription.add( media.subscribe((change: MediaChange) => {
      if (change.mqAlias === 'sm' || change.mqAlias === 'xs') {
        this.opened = false;
        this.over = 'over';
      } else {
        if(!this.manuallyToggled){
          this.opened = true;
        }
        this.over = 'side';
      }
    }));
  }

  ngOnInit() {
    this.friendList$ = this.friendService.friendsSubject;
    this.loggedUser$ = this.authService.loggedUserSubject;
    this.subscription.add(this.loggedUser$.subscribe((user: User) => {
      if (user) {
        this.friendService.updateFriendlist(user.id);
        this.imageService.getUserProfilePicture(user.id).subscribe((image: Image) => {
            this.profilePictureUrl = image.mediaUrl;
        });
      }
    }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
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

  public isAdmin() {
    return this.authService.isAdmin;
  }

}
