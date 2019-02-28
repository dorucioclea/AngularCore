import { FriendService } from '@app/services/friend.service';
import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { User } from '@app/models/user';
import { Image } from '@app/models/image';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.scss']
})
export class FriendListComponent implements OnInit, OnChanges {

  @Input() friends: User[];

  private defaultProfileSrc = "../../../assets/images/default-profile-pic.png";

  constructor(private friendService: FriendService) { }

  ngOnInit() {
    this.setDefaultProfilePictures();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.friends) {
      this.friends = changes.friends.currentValue;
      this.setDefaultProfilePictures();
    }
  }

  private setDefaultProfilePictures() {
    if (!this.friends) {
      return;
    }

    this.friends.forEach(friend => {
      if (!friend.profilePicture) {
        let noProfile = new Image();
        noProfile.mediaUrl = this.defaultProfileSrc;
        friend.profilePicture = noProfile;
      }
    })
  }

  public removeFriend(friendId: string){
    this.friendService.removeFriend(friendId);
  }

}
