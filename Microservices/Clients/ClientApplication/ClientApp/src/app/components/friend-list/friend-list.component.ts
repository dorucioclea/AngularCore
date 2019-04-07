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

  constructor(private friendService: FriendService) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.friends) {
      this.friends = changes.friends.currentValue;
    }
  }

  public removeFriend(friendId: string){
    this.friendService.removeFriend(friendId);
  }

}
