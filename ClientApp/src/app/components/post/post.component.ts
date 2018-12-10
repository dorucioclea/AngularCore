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
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent {

  postForm: PostForm;

  constructor(private http: HttpClient,
              private router: Router,
              private authService: AuthService,
              private spinnerService: SpinnerOverlayService) { }

  addPost(postForm: NgForm) {
    this.spinnerService.show();
    this.postForm = postForm.value;
    this.postForm.ownerId = this.authService.loggedUser.id;
    this.http.post<Post>("/api/Post/CreatePost", this.postForm).toPromise()
      .then( result => {
        this.router.navigate(["/"]);
        this.spinnerService.hide();
      })
  }

}
