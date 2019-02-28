import { AuthService } from '@app/services/auth.service';
import { FriendService } from '@app/services/friend.service';
import { User } from '@app/models/user';
import { Image } from '@app/models/image';
import { Component, OnInit, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html'
})
export class UserProfileComponent implements OnInit, OnDestroy, OnChanges {

  @Input() user: User;

  private subscription: Subscription = new Subscription();
  private profilePictureUrl: string = "../../../assets/images/default-profile-pic.png"
  isInFriendList: boolean;
  selfProfile: boolean;


  constructor(
    private friendService: FriendService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    if (!this.user.profilePicture) {
      let defaultImage = new Image();
      defaultImage.mediaUrl = this.profilePictureUrl;
      this.user.profilePicture = defaultImage;
    }

    this.subscription.add(
      this.friendService.friendsSubject.subscribe( () => {
        if(this.user) {
          this.checkIfInFriendList(this.user.id);
        }
      })
    )

    this.subscription.add(
      this.authService.loggedUserSubject.subscribe( (user: User) => {
        if(this.user && user) {
          this.selfProfile = this.user.id === user.id;
        }
      })
    )
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  ngOnChanges( changes: SimpleChanges) {
    let user = changes.user.currentValue;
    if (!user.profilePicture) {
      let defaultImage = new Image();
      defaultImage.mediaUrl = this.profilePictureUrl;
      this.user.profilePicture = defaultImage;
    }
    this.user = user;
    let loggedUser = this.authService.loggedUserValue;
    this.checkIfInFriendList(user.id);
    this.selfProfile = loggedUser ? user.id === loggedUser.id : false;
  }

  addFriend() {
    this.friendService.addFriend(this.user.id);
    this.checkIfInFriendList(this.user.id);
  }

  removeFriend() {
    this.friendService.removeFriend(this.user.id);
    this.checkIfInFriendList(this.user.id);
  }

  private checkIfInFriendList(userId: string) {
    this.isInFriendList = this.friendService.userInFriendList(userId);
  }

}
