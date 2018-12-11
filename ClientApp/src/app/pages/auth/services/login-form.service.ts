import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class LoginFormService {

  errorMessage: string;
  loginForm: FormGroup;
  submitted = false;

  constructor() { }
}
