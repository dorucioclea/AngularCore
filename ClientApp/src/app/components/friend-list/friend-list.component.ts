import { FriendService } from '@app/services/friend.service';
import { Component, OnInit, Input } from '@angular/core';
import { User } from '@app/models/user';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.scss']
})
export class FriendListComponent implements OnInit {

  @Input() friends: User[];

  constructor(private friendService: FriendService) { }

  ngOnInit() {
  }

  public removeFriend(friendId: string){
    this.friendService.removeFriend(friendId);
  }

}
