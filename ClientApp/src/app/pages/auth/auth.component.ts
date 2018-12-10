import { LoggedUser } from '../../models/logged-user';
import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  loginForm: FormGroup;
  registerForm: FormGroup;
  returnUrl: string;
  loginSubmitted = false;
  registerSubmitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email:    ['', [ Validators.required, Validators.email ] ],
      password: ['', Validators.required]
    });

    this.registerForm = this.formBuilder.group({
      name:           ['', Validators.required],
      surname:        ['', Validators.required],
      email:          ['', [ Validators.required, Validators.email ] ],
      password:       ['', Validators.required],
      passwordCheck:  ['', Validators.required]
    });

    this.authService.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

  }

  get loginFields() {
    return this.loginForm.controls;
  }

  get registerFields() {
    return this.registerForm.controls;
  }

  public onLogin() {
    this.loginSubmitted = true;
    if(this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value).pipe(first()).subscribe( (user : LoggedUser) => {
      console.log("User logged in successfuly!\n", user)
      this.redirectToReturn();
    });
  }

  public onRegister() {
    this.registerSubmitted = true;
    if(this.registerForm.invalid) {
      return;
    }

    this.authService.register(this.registerForm.value).pipe(first()).subscribe( (user : LoggedUser) => {
      console.log("User registered successfuly!\n", user)
      this.redirectToReturn();
    })
    console.log("Register: ", this.registerFields);
  }

  private redirectToReturn(){
    this.router.navigate([this.returnUrl]);
  }

}
