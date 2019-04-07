import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { WallComponent } from '@app/pages/wall/wall.component';
import { AuthComponent } from '@app/pages/auth/auth.component';
import { AuthGuard } from '@app/guards/auth.guard';
import { UsersComponent } from '@app/pages/admin/users/users.component';
import { PostsComponent } from '@app/pages/admin/posts/posts.component';
import { PhotosComponent } from '@app/pages/admin/photos/photos.component';

const routes: Routes = [
  { path: '', component: WallComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'auth', component: AuthComponent },
  { path: 'admin', canActivate: [AuthGuard], children: [
      { path: '', component: WallComponent },
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
