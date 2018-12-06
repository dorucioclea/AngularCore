import { AuthInterceptor } from './interceptors/auth-interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { AuthComponent } from './components/auth/auth.component';
import { SpinnerOverlayService } from './services/spinner-overlay.service';
import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import { PostComponent } from './components/post/post.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AuthComponent,
    SpinnerOverlayComponent,
    PostComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'auth', component: AuthComponent },
      { path: 'post', component: PostComponent }
    ])
  ],
  providers: [
    AuthService,
    SpinnerOverlayService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
