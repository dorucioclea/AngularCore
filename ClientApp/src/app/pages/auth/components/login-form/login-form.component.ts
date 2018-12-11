import { LoginFormService } from '@app/pages/auth/services/login-form.service';
import { LoginResponse } from '@app/models/login-response';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '@app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  @Input() redirectRoute: string;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public loginFormService: LoginFormService
  ) { }

  ngOnInit() {
    this.loginFormService.loginForm = this.formBuilder.group({
      email:    ['', [ Validators.required, Validators.email ] ],
      password: ['', Validators.required]
    });
  }

  get errorMessage()  { return this.loginFormService.errorMessage }
  get loginForm()     { return this.loginFormService.loginForm }
  get submitted()     { return this.loginFormService.submitted }
  get fields()        { return this.loginForm.controls }

  public onLogin() {
    this.loginFormService.submitted = true;
    this.loginFormService.errorMessage = '';
    if(this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value).subscribe(
      (user : LoginResponse) => {
        console.log("User logged in successfuly!\n", user)
        this.redirectToReturn();
      }, (err) => {
        this.loginFormService.errorMessage = err;
    });
  }

  private redirectToReturn(){
    this.router.navigate([this.redirectRoute]);
  }

}
