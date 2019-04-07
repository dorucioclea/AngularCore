import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Post } from '@app/models/post';
import { Image } from '@app/models/image';

@Injectable({
  providedIn: 'root'
})
export class UserService{

    private readonly apiUrl = "http://localhost:16001/api";
    private readonly userUrl = this.apiUrl + "/users/";
    private readonly postsUrl = this.apiUrl + "/posts";

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  public getUser(id: string) {
    return this.http.get<User>(this.userUrl + id);
  }

  public getAllUsers() {
    return this.http.get<User[]>(this.userUrl);
  }

  public getUserPosts(userId?: string) {
    if(!userId) {
      userId = this.authService.loggedUserValue.id;
    }
    return this.http.get<Post[]>(this.postsUrl + "/wall/" + userId);
  }

  public getUserImages(userId?: string) {
    if (!userId) {
      userId = this.authService.loggedUserValue.id;
    }
    return this.http.get<Image[]>(this.userUrl + userId + "/images");
  }

  public deleteUser(userId: string) {
    return this.http.delete(this.userUrl + userId);
  }
}
