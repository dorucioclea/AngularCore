import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Post } from '@app/models/post';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html'
})
export class PostListComponent implements OnInit, OnChanges {

  @Input() posts: Post[];

  constructor() { }

  ngOnInit() {
    this.posts = this.posts.sort(this.sortByDateFunc());
  }

  ngOnChanges( changes: SimpleChanges ) {
    this.posts = changes.posts.currentValue.sort(this.sortByDateFunc());
  }

  private sortByDateFunc() {
    return ( p1: Post, p2: Post) => {
      return new Date(p1.createdAt).getTime() - new Date(p2.createdAt).getTime();
    }
  }

}
