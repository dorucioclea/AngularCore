import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { NgModule, APP_INITIALIZER, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

export function init_user(authService: AuthService) {
  if(authService.authToken) {
    return authService.renewOrClearSession();
  }
  return undefined;
}

@NgModule({
  imports: [HttpClientModule],
  providers: [
    AuthService,
    { provide: APP_INITIALIZER, useFactory: init_user, deps: [AuthService], multi: true }
  ]
})
export class AppLoadModule { }
