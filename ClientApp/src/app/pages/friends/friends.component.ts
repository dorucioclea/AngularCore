import { FriendService } from '@app/services/friend.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { User } from '@app/models/user';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit, OnDestroy {

  friendList: User[];
  subscription: Subscription;

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

  public removeFriend(friendId: string) {
    this.friendService.removeFriend(friendId);
  }

}
