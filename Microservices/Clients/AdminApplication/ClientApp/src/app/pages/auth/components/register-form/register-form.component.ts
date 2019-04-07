import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '@app/services/auth.service';
import { Router } from '@angular/router';
import { LoginResponse } from '@app/models/login-response';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html'
})
export class RegisterFormComponent implements OnInit {

  @Input() redirectRoute: string;

  errorMessage: string;
  registerForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstname:      ['', Validators.required],
      lastname:       ['', Validators.required],
      email:          ['', [ Validators.required, Validators.email ] ],
      password:       ['', Validators.required],
      passwordCheck:  ['', Validators.required]
    });
  }

  get fields()        { return this.registerForm.controls }

  public onRegister() {
    this.submitted = true;
    this.errorMessage = '';
    if(this.registerForm.invalid) {
      return;
    }

    this.authService.register(this.registerForm.value).subscribe(
      (user : LoginResponse) => {
        console.log("User registered successfuly!\n", user)
        this.redirectToReturn();
      }, (err) => {
        this.errorMessage = err;
    })
  }

  private redirectToReturn(){
    this.router.navigate([this.redirectRoute]);
  }
}
