import { SnackService } from './snack.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { User } from './../models/user';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  public friendsSubject: BehaviorSubject<User[]>;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private snackService: SnackService
  ) {
    this.friendsSubject = new BehaviorSubject<User[]>(undefined);
  }

  private friendsUrl(id: string) {
    return `/api/v1/users/${id}/friends/`;
  }

  public getUserFriendlist(userId: string) {
    return this.http.get<User[]>(this.friendsUrl(userId));
  }

  public updateFriendlist(userId: string) {
    this.getUserFriendlist(userId).subscribe( (friends) => {
      this.friendsSubject.next(friends);
    })
  }

  public addFriend(friendId: string) {
    let userId = this.authService.loggedUserValue.id;
    this.http.post(this.friendsUrl(userId), JSON.stringify(friendId)).subscribe( () => {
      this.updateFriendlist(userId);
      this.snackService.showBar("Friend added!");
    }, () => {
      this.snackService.showBar("Failed to add a friend");
    });
  }

  public removeFriend(friendId: string) {
    let userId = this.authService.loggedUserValue.id;
    this.http.delete(this.friendsUrl(userId) + friendId).subscribe( () => {
      this.updateFriendlist(userId);
      this.snackService.showBar("Friend removed!");
    }, () => {
      this.snackService.showBar("Failed to remove a friend");
    });
  }

  public userInFriendList(friendId: string) : boolean {
    let currentList = this.friendsSubject.value;
    if(!currentList) {
      return false;
    }

    let result = currentList.some( f => f.id === friendId);
    return result;
  }

}
