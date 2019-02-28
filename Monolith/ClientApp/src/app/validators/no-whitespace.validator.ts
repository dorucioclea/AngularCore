import { AbstractControl } from '@angular/forms';

export function noWhitespaceValidator(control: AbstractControl): { [key:string]: boolean } | null {
  let isValid = (control.value || '').trim().length > 0;
  return isValid ? null : { "whitespace" : false };
}
