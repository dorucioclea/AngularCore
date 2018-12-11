import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '@app/services/auth.service';
import { Router } from '@angular/router';
import { LoginResponse } from '@app/models/login-response';
import { RegisterFormService } from '@app/pages/auth/services/register-form.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent implements OnInit {

  @Input() redirectRoute: string;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public registerFormService: RegisterFormService
  ) { }

  ngOnInit() {
    this.registerFormService.registerForm = this.formBuilder.group({
      name:           ['', Validators.required],
      surname:        ['', Validators.required],
      email:          ['', [ Validators.required, Validators.email ] ],
      password:       ['', Validators.required],
      passwordCheck:  ['', Validators.required]
    });
  }

  get errorMessage()  { return this.registerFormService.errorMessage }
  get registerForm()  { return this.registerFormService.registerForm }
  get submitted()     { return this.registerFormService.submitted }
  get fields()        { return this.registerForm.controls }

  public onRegister() {
    this.registerFormService.submitted = true;
    this.registerFormService.errorMessage = '';
    if(this.registerForm.invalid) {
      return;
    }

    this.authService.register(this.registerForm.value).subscribe(
      (user : LoginResponse) => {
        console.log("User registered successfuly!\n", user)
        this.redirectToReturn();
      }, (err) => {
        this.registerFormService.errorMessage = err;
    })
  }

  private redirectToReturn(){
    this.router.navigate([this.redirectRoute]);
  }
}
