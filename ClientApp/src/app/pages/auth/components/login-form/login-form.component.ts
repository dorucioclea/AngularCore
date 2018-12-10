import { Component, Input, Output, EventEmitter } from '@angular/core';
import { LoginForm } from '@app/models/login-form';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {

  @Input('form') loginForm: LoginForm;
  @Input('submit') submitFunction: Function;

  @Output() formChange = new EventEmitter();

  submit() {
    this.formChange.emit( this.loginForm );
  }

}
