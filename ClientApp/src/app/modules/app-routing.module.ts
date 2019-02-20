import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { WallComponent } from '@app/pages/client/wall/wall.component';
import { AuthComponent } from '@app/pages/auth/auth.component';
import { ProfileComponent } from '@app/pages/client/profile/profile.component';
import { FriendsComponent } from '@app/pages/client/friends/friends.component';
import { AuthGuard } from '@app/guards/auth.guard';
import { UsersComponent } from '@app/pages/admin/users/users.component';
import { PostsComponent } from '@app/pages/admin/posts/posts.component';
import { PhotosComponent } from '@app/pages/admin/photos/photos.component';
import { AdminGuard } from '@app/guards/admin.guard';

const routes: Routes = [
  { path: '', component: WallComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'auth', component: AuthComponent },
  { path: 'profile/:id', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'friends', component: FriendsComponent, canActivate: [AuthGuard] },
  { path: 'admin', canActivate: [AdminGuard], children: [
      { path: 'users', component: UsersComponent },
      { path: 'posts', component: PostsComponent },
      { path: 'photos', component: PhotosComponent }
  ]},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
