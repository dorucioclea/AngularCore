import { Post } from '../../models/post';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PostService } from '@app/services/post.service';

@Component({
  selector: 'app-wall',
  templateUrl: './wall.component.html',
})
export class WallComponent implements OnInit {

  posts: Post[];

  constructor(private http: HttpClient,
              private postService: PostService){ }

  ngOnInit() {
    this.postService.getAllPosts().toPromise()
      .then( result => {
        this.posts = result;
      })
  }

}
