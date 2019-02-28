import { UserService } from '@app/services/user.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { User } from '@app/models/user';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '@app/services/auth.service';
import { Post } from '@app/models/post';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public profile$: Observable<User>;
  public userPosts$: Observable<Post[]>;
  public currentUser: boolean;
  public postsTabSelected: boolean = true;
  public galleryTabSelected: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.profile$ = this.route.paramMap.pipe(
      switchMap( (params: ParamMap) => {
        return this.userService.getUser(params.get('id'));
      })
    );

    this.profile$.subscribe(result => {
      if (this.authService.loggedUserValue) {
        this.currentUser = result.id === this.authService.loggedUserValue.id;
      }
      this.userPosts$ = this.userService.getUserPosts(result.id);

    });
  }

  public selectPostsTab() {
    this.postsTabSelected = true;
    this.galleryTabSelected = false;
  }

  public selectGalleryTab() {
    this.galleryTabSelected = true;
    this.postsTabSelected = false;
  }

}
