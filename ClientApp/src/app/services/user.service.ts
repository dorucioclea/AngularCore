import { FriendUser } from './../models/friend-user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userUrl = "/api/User";

  constructor(
    private http: HttpClient
  ) { }

  public getUser(id: string) {
    return this.http.get<User>(this.userUrl + "/GetUser/" + id).toPromise();
  }

  public getUserFriends(id: string) {
    return this.http.get<FriendUser[]>(this.userUrl + "/GetUserFriends/" + id);
  }

  public getAllUsers() {
    return this.http.get<User[]>(this.userUrl + "/GetAllUsers");
  }

  public addFriend(friend: User) {
    return this.http.post(this.userUrl + "/AddFriend", friend).toPromise()
    .catch( error => {
      console.warn("Friend add request failed: ", error);
    })
  }

  public removeFriend(friend: FriendUser) {
    return this.http.post(this.userUrl + "/RemoveFriend", friend).toPromise()
    .catch( error => {
      console.warn("Remove friend request failed: ", error);
    })
  }

}
