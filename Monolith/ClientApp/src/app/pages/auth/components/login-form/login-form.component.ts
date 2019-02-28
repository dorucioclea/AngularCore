import { LoginResponse } from '@app/models/login-response';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '@app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html'
})
export class LoginFormComponent implements OnInit {

  @Input() redirectRoute: string;

  errorMessage: string;
  loginForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email:    ['', [ Validators.required, Validators.email ] ],
      password: ['', Validators.required]
    });
  }

  get fields()        { return this.loginForm.controls }

  public onLogin() {
    this.submitted = true;
    this.errorMessage = '';
    if(this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value).subscribe(
      (user : LoginResponse) => {
        console.log("User logged in successfuly!\n", user)
        this.redirectToReturn();
      }, (err) => {
        this.errorMessage = err;
    });
  }

  private redirectToReturn(){
    this.router.navigate([this.redirectRoute]);
  }

}
