import { HttpErrorResponse } from '@angular/common/http';
import { PostService } from './../../services/post.service';
import { UserService } from './../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { switchMap, catchError } from 'rxjs/operators';
import { User } from '../../models/user';
import { Observable, ObservableInput } from 'rxjs';
import { Post } from '@app/models/post';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  profile$: Observable<User>;
  posts$: Observable<Post[]>;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private postService: PostService
  ) { }

  ngOnInit() {
    this.profile$ = this.route.paramMap.pipe(
      switchMap( params => {
        return this.userService.getUser(params.get('id'));
      })
    );
    this.posts$ = this.route.paramMap.pipe(
      switchMap( params => {
        return this.postService.getUserPosts(params.get('id')).pipe(
          catchError(this.handleError)
        );
      })
    )
  }

  private handleError(err: HttpErrorResponse): ObservableInput<Post[]>{
    if( err.status === 204 ) {
      return [];
    } else {
      console.warn(err);
    }
  }

}
