import { UserService } from '@app/services/user.service';
import { SpinnerOverlayService } from '@app/services/spinner-overlay.service';
import { AuthService } from '@app/services/auth.service';
import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { noWhitespaceValidator } from '@app/validators/no-whitespace.validator';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html'
})
export class PostComponent implements OnInit {

  @Input() wallOwnerId: string;

  postForm: FormGroup;

  constructor(private router: Router,
              private spinnerService: SpinnerOverlayService,
              private userService: UserService,
              private formBuilder: FormBuilder,
              private authService: AuthService) { }

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      authorId:     [ this.authService.loggedUserValue.id, [Validators.required] ],
      wallOwnerId:  [ this.wallOwnerId, [Validators.required] ],
      content:    [ undefined, [noWhitespaceValidator] ]
    });
  }

  addPost() {
    if(this.postForm.valid){
      this.spinnerService.show();
      this.userService.createUserPost(this.postForm.value, this.wallOwnerId).subscribe(
        () => {
          this.router.navigate(["/"]);
          this.spinnerService.hide();
        }
      )
    }
  }

}
