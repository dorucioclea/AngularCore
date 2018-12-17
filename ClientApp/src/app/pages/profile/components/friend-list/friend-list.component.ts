import { FriendService } from './../../../../services/friend.service';
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

  public removeFriend(friend: User){
    this.friendService.removeFriend(friend);
  }

}
