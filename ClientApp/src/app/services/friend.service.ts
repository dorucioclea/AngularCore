import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { User } from './../models/user';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  private friendsUrl(id: string) {
    return `/api/v1/users/${id}/friends/`;
  }

  public getUserFriendlist(userId: string) {
    return this.http.get<User[]>(this.friendsUrl(userId));
  }

  public addFriend(friendId: string) {
    let userId = this.authService.loggedUserValue.id;
    return this.http.post(this.friendsUrl(userId), JSON.stringify(friendId));
  }

  public removeFriend(friendId: string) {
    let userId = this.authService.loggedUserValue.id;
    return this.http.delete(this.friendsUrl(userId) + friendId);
  }

}
