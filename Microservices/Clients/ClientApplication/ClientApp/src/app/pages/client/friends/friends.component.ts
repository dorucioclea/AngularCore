import { FriendService } from '@app/services/friend.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { User } from '@app/models/user';
import { Image } from '@app/models/image';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit, OnDestroy {

  friendList: User[];
  subscription: Subscription;

  private defaultProfileSrc = "../../../assets/images/default-profile-pic.png";

  constructor(
    private friendService: FriendService
  ) { }

  ngOnInit() {
    this.subscription = this.friendService.friendsSubject.subscribe( (friends) => {
      this.friendList = friends;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  private setDefaultProfilePictures() {
    if (!this.friendList) {
      return;
    }

    this.friendList.forEach(friend => {
      if (!friend.profilePicture) {
        let noProfile = new Image();
        noProfile.mediaUrl = this.defaultProfileSrc;
        friend.profilePicture = noProfile;
      }
    })
  }

  public removeFriend(friendId: string) {
    this.friendService.removeFriend(friendId);
  }

}
