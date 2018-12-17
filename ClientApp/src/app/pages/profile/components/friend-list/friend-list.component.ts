import { Component, OnInit, Input } from '@angular/core';
import { FriendUser } from '@app/models/friend-user';
import { UserService } from '@app/services/user.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.scss']
})
export class FriendListComponent implements OnInit {

  @Input() friends: FriendUser[];

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  public removeFriend(friend: FriendUser){
    this.userService.removeFriend(friend).then( (result: any) => {
      this.friends = this.friends.filter( f => f.id !== friend.id);
    })
  }

}
