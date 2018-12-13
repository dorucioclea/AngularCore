import { Component, OnInit, Input } from '@angular/core';
import { FriendUser } from '@app/models/friend-user';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html'
})
export class FriendListComponent implements OnInit {

  @Input() friends: FriendUser[];

  constructor() { }

  ngOnInit() {
  }

}
