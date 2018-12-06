import { Post } from './../../models/post';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  posts: Post[];

  constructor(private http: HttpClient){ }

  ngOnInit() {
    this.http.get<Post[]>("/api/Post/GetAllPosts").toPromise()
      .then( result => {
        this.posts = result;
      })
  }

}
