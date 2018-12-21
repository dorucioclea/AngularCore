import { FriendService } from './../../services/friend.service';
import { User } from '../../models/user';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html'
})
export class TestingComponent {
  users: User[];
  authTest = false;
  authErrorText: string;

  constructor(
    private http: HttpClient,
    private friendService: FriendService
  ) { }

  public testAuthEndpoint() {
    this.authTest = true;
    this.http.get<User[]>('/api/v1/users/').toPromise()
    .then( response => {
      this.users = response;
    }).catch( error => {
      this.authErrorText = error.message;
    })
  }

  public addFriend(friendId: string) {
    this.friendService.addFriend(friendId).subscribe( () => {
      console.log("Friend added!");
    });
  }

}
