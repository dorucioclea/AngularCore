import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '@app/models/post';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  postUrl = "/api/Post";

  constructor(
    private http: HttpClient
  ) { }

  public getUserPosts(userId: string) {
    return this.http.get<Post[]>(this.postUrl + "/GetUserPosts/" + userId).pipe( first() );
  }
}
