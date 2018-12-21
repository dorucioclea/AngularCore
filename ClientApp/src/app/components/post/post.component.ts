import { UserService } from './../../services/user.service';
import { SpinnerOverlayService } from './../../services/spinner-overlay.service';
import { AuthService } from './../../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { PostForm } from '../../models/post-form';
import { NgForm } from '@angular/forms';
import { Post } from '../../models/post';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html'
})
export class PostComponent {

  postForm: PostForm;

  constructor(private router: Router,
              private spinnerService: SpinnerOverlayService,
              private userService: UserService) { }

  addPost(postForm: NgForm) {
    this.spinnerService.show();
    this.userService.createUserPost(postForm.value).toPromise()
      .then( () => {
        this.router.navigate(["/"]);
        this.spinnerService.hide();
      })
  }

}
