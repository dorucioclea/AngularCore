import { Component, Input, EventEmitter, Output } from '@angular/core';
import { RegisterForm } from '@app/models/register-form';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {

  @Input('form') registerForm: RegisterForm;
  @Input('submit') submitFunction: Function;

  @Output() formChange = new EventEmitter();

  submit() {
    this.formChange.emit( this.registerForm );
  }

}
