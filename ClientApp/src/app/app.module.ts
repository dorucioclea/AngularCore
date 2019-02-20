import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { WallComponent } from './pages/client/wall/wall.component';
import { AuthComponent } from './pages/auth/auth.component';

import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { SharedUIModule } from './modules/shared-ui.module';
import { PostComponent } from './pages/client/profile/components/post/post.component';
import { AppComponent } from './app.component';
import { ProfileComponent } from './pages/client/profile/profile.component';
import { LoginFormComponent } from './pages/auth/components/login-form/login-form.component';
import { RegisterFormComponent } from './pages/auth/components/register-form/register-form.component';
import { UserProfileComponent } from './pages/client/profile/components/user-profile/user-profile.component';
import { PostListComponent } from './components/post-list/post-list.component';
import { FriendListComponent } from './components/friend-list/friend-list.component';
import { FriendsComponent } from './pages/client/friends/friends.component';
import { AppRoutingModule } from './modules/app-routing.module';
import { AppLoadModule } from './modules/app-load.module';
import { ImageUploadComponent } from './components/image-upload/image-upload.component';
import { ImageUploadDialogComponent } from './components/image-upload/image-upload-dialog/image-upload-dialog.component';
import { ImageGalleryComponent } from './pages/client/profile/components/image-gallery/image-gallery.component';
import { ImageTileComponent } from './pages/client/profile/components/image-gallery/image-tile/image-tile.component';
import { ImageViewComponent } from './pages/client/profile/components/image-gallery/image-tile/image-view/image-view.component';
import { SearchBarComponent } from './components/nav-menu/search-bar/search-bar.component';
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
    PostComponent,
    ProfileComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UserProfileComponent,
    PostListComponent,
    FriendListComponent,
    FriendsComponent,
    ImageUploadComponent,
    ImageUploadDialogComponent,
    ImageGalleryComponent,
    ImageTileComponent,
    ImageViewComponent,
    SearchBarComponent,
    UsersComponent,
    PostsComponent,
    PhotosComponent
  ],
  entryComponents: [
    ImageUploadDialogComponent,
    ImageViewComponent
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
