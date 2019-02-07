import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Post } from '@app/models/post';
import * as moment from 'moment';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html'
})
export class PostListComponent implements OnInit, OnChanges {

  @Input() posts: Post[] = new Array<Post>();
  @Input() showRecipient: boolean;

  constructor() { }

  ngOnInit() {
    if (this.posts) {
      this.posts = this.posts.sort(this.sortByDateFunc());
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.posts.currentValue) {
      this.posts = changes.posts.currentValue.sort(this.sortByDateFunc());
    }
    this.posts = changes.posts.currentValue;
  }

  private sortByDateFunc() {
    return (p1: Post, p2: Post) => {
      return new Date(p2.createdAt).getTime() - new Date(p1.createdAt).getTime();
    }
  }

}
