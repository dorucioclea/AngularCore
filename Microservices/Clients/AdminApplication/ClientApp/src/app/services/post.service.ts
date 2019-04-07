import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '@app/models/post';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  postUrl = "http://localhost:16001/api/posts/";

  constructor(
    private http: HttpClient
  ) { }

  public getAllPosts() {
    return this.http.get<Post[]>(this.postUrl).pipe( first() );
  }

  public deletePost(postId) {
    return this.http.delete(this.postUrl + postId);
  }
}
