import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Post } from '@app/models/post';
import { Image } from '@app/models/image';
import * as moment from 'moment';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit, OnChanges {

  @Input() posts: Post[] = new Array<Post>();
  @Input() showRecipient: boolean;

  constructor() { }

  ngOnInit() {
    if (this.posts) {
      this.posts = this.posts.sort(this.sortByDateFunc());
      console.log(this.posts);
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.posts && changes.posts.currentValue) {
      this.posts = changes.posts.currentValue.sort(this.sortByDateFunc());
    }
  }

  private sortByDateFunc() {
    return (p1: Post, p2: Post) => {
      var format = "DD.MM.YYYY HH:mm:ss";
      var p1Date = moment(p1.createdAt, format);
      var p2Date = moment(p2.createdAt, format);
      return p2Date.isBefore(p1Date) ? -1 : 1;
    }
  }

}
