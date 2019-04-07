import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { WallComponent } from './pages/wall/wall.component';
import { AuthComponent } from './pages/auth/auth.component';

import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SharedUIModule } from './modules/shared-ui.module';
import { AppComponent } from './app.component';
import { LoginFormComponent } from './pages/auth/components/login-form/login-form.component';
import { RegisterFormComponent } from './pages/auth/components/register-form/register-form.component';
import { AppRoutingModule } from './modules/app-routing.module';
import { AppLoadModule } from './modules/app-load.module';
import { DateFormatPipe } from './pipes/date-format.pipe';
import { UsersComponent } from './pages/admin/users/users.component';
import { PostsComponent } from './pages/admin/posts/posts.component';
import { PhotosComponent } from './pages/admin/photos/photos.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    WallComponent,
    AuthComponent,
    SpinnerOverlayComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UsersComponent,
    PostsComponent,
    PhotosComponent,
    DateFormatPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    SharedUIModule,
    AppRoutingModule,
    // AppLoadModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
