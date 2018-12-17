import { HttpClient } from '@angular/common/http';
import { User } from './../models/user';
import { AuthService } from '@app/services/auth.service';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  private userUrl = "/api/User";

  public friendList$ = new BehaviorSubject(undefined);
  private friendList : User[];

  constructor(
    private authService: AuthService,
    private http: HttpClient
  ) {
    if( authService.isLoggedIn ) {
      let id = authService.loggedUser.id;
      this.getUserFriendlist(id).subscribe( value => {
        this.friendList = value != undefined ? value : [];
        this.friendList$.next(this.friendList);
      })
    }
  }

  public getUserFriendlist(userId: string) {
    return this.http.get<User[]>(this.userUrl + "/GetUserFriends/" + userId);
  }

  public addFriend(user: User) {
    return this.http.post(this.userUrl + "/AddFriend", user).subscribe( () => {
      this.friendList.push(user);
      this.friendList.sort();
      this.friendList$.next(this.friendList);
    })

  }

  public removeFriend(user: User) {
    return this.http.post(this.userUrl + "/RemoveFriend", user).subscribe( () => {
      this.friendList.filter( friend => friend.id !== user.id );
      this.friendList$.next(this.friendList);
    })

  }

}
