import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '@app/models/post';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  postUrl = "/api/v1/posts";

  constructor(
    private http: HttpClient
  ) { }

  public getAllPosts() {
    return this.http.get<Post[]>(this.postUrl).pipe( first() );
  }
}
