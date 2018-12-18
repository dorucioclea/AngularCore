import { FriendService } from './../../services/friend.service';
import { HttpErrorResponse } from '@angular/common/http';
import { PostService } from './../../services/post.service';
import { UserService } from './../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { switchMap, catchError, tap } from 'rxjs/operators';
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
  friends$: Observable<User[]>;
  posts$: Observable<Post[]>;
  postsLoaded: boolean;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private friendService: FriendService,
    private postService: PostService
  ) { }

  ngOnInit() {
    this.postsLoaded = false;
    this.profile$ = this.route.paramMap.pipe(
      switchMap( params => {
        return this.userService.getUser(params.get('id'));
      })
    );

    this.profile$.subscribe( (user: User) => {
      this.friends$ = this.friendService.getUserFriendlist(user.id);
      this.posts$ = this.postService.getUserPosts(user.id).pipe( catchError( this.handleError ) )
      this.posts$.subscribe( () => {
        this.postsLoaded = true;
      })
    });
  }

  private handleError(err: HttpErrorResponse): ObservableInput<Post[]>{
    if( err.status === 204 ) {
      return [];
    } else {
      console.warn(err);
    }
  }

}
