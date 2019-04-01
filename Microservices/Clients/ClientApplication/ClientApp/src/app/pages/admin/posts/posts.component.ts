import { Component, OnInit } from '@angular/core';
import { PostService } from '@app/services/post.service';
import { SnackService } from '@app/services/snack.service';
import { Observable } from 'rxjs';
import { Post } from '@app/models/post';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {

  public postsList$: Observable<Post[]>;

  constructor(
    private postService: PostService,
    private snackService: SnackService
  ) { }

  ngOnInit() {
    this.postsList$ = this.postService.getAllPosts();
  }

  deletePost(postId) {
    this.postService.deletePost(postId).subscribe(success => {
      this.snackService.showBar("Post deleted successfully!");
      this.postsList$ = this.postService.getAllPosts();
    }, error => {
      this.snackService.showBar("Error occured while deleting post! Sorry!");
    })
  }

}
