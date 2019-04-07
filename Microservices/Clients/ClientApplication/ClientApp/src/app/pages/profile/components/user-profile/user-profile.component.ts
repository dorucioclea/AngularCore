import { AuthService } from '@app/services/auth.service';
import { FriendService } from '@app/services/friend.service';
import { User } from '@app/models/user';
import { Image } from '@app/models/image';
import { Component, OnInit, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ImageService } from '../../../../services/image.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html'
})
export class UserProfileComponent implements OnInit, OnDestroy, OnChanges {

  @Input() user: User;

  private subscription: Subscription = new Subscription();
  isInFriendList: boolean;
  selfProfile: boolean;
  public profilePictureUrl: string;


  constructor(
    private friendService: FriendService,
    private authService: AuthService,
    private imageService: ImageService
  ) { }

  ngOnInit() {
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

    this.imageService.getUserProfilePicture(this.user.id).subscribe((image: Image) => {
        this.profilePictureUrl = image.mediaUrl;
    })
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  ngOnChanges( changes: SimpleChanges) {
    this.user = changes.user.currentValue;
    let loggedUser = this.authService.loggedUserValue;
    this.checkIfInFriendList(this.user.id);
    this.selfProfile = loggedUser ? this.user.id === loggedUser.id : false;
    this.imageService.getUserProfilePicture(this.user.id).subscribe((image: Image) => {
        this.profilePictureUrl = image.mediaUrl;
    })
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
