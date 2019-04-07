import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { WallComponent } from '@app/pages/wall/wall.component';
import { AuthComponent } from '@app/pages/auth/auth.component';
import { ProfileComponent } from '@app/pages/profile/profile.component';
import { FriendsComponent } from '@app/pages/friends/friends.component';
import { AuthGuard } from '@app/guards/auth.guard';

const routes: Routes = [
  { path: '', component: WallComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'auth', component: AuthComponent },
  { path: 'profile/:id', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'friends', component: FriendsComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
