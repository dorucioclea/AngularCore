import { User } from './../../../../models/user';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html'
})
export class UserProfileComponent implements OnInit {

  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
