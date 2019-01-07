import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { WallComponent } from './pages/wall/wall.component';
import { TestingComponent } from './pages/testing/testing.component';
import { AuthComponent } from './pages/auth/auth.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { FriendsComponent } from './pages/friends/friends.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', component: WallComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'testing', component: TestingComponent, canActivate: [AuthGuard] },
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
