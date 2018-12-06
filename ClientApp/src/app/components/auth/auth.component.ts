import { LoggedUser } from './../../models/logged-user';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginForm } from '../../models/login-form';
import { RegisterForm } from '../../models/register-form';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {

  loginForm = new LoginForm();
  registerForm = new RegisterForm();

  constructor(private authService: AuthService, private router: Router) { }

  private onLogin(form: NgForm) {
    this.authService.login(form.value).subscribe( (user : LoggedUser) => {
      console.log("User logged in successfuly!\n", user)
      this.redirectHome();
    });
  }

  private onRegister(form: NgForm) {
    this.authService.register(form.value).subscribe( (user : LoggedUser) => {
      console.log("User registered successfuly!\n", user)
      this.redirectHome();
    })
    console.log("Register: ", form.value);
  }

  private redirectHome(){
    this.router.navigate(['/']);
  }

}
