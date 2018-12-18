import { HttpClient } from '@angular/common/http';
import { User } from './../models/user';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  private userUrl = "/api/User";

  constructor(
    private http: HttpClient
  ) { }

  public getUserFriendlist(userId: string) {
    return this.http.get<User[]>(this.userUrl + "/GetUserFriends/" + userId);
  }

  public addFriend(user: User) {
    return this.http.post(this.userUrl + "/AddFriend", user);
  }

  public removeFriend(user: User) {
    return this.http.post(this.userUrl + "/RemoveFriend", user);
  }

}
