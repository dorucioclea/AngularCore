import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Post } from '@app/models/post';
import { PostForm } from '@app/models/post-form';
import { Image } from '@app/models/image';
import { ProfilePictureUpdate } from '@app/models/profile-picture-update';

@Injectable({
  providedIn: 'root'
})
export class UserService{

  private userUrl = "/api/v1/users/";

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
    return this.http.get<Post[]>(this.userUrl + userId + "/posts");
  }

  public getUserImages(userId?: string) {
    if (!userId) {
      userId = this.authService.loggedUserValue.id;
    }
    return this.http.get<Image[]>(this.userUrl + userId + "/images");
  }

  public createUserPost(post: PostForm, userId?: string) {
    if(!userId) {
      userId = this.authService.loggedUserValue.id;
    }
    return this.http.post<Post>(this.userUrl + userId + "/posts", post);
  }

  public setUserProfilePicture(imageId: string, userId?: string) {
    if (!userId) {
      userId = this.authService.loggedUserValue.id;
    }
    var imageUpdate = new ProfilePictureUpdate(imageId = imageId);
    return this.http.post(this.userUrl + userId + "/avatar", imageUpdate );
  }

  public deleteUser(userId: string) {
    return this.http.delete(this.userUrl + userId);
  }
}
