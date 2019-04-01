import { Component, OnInit } from '@angular/core';
import { UserService } from '@app/services/user.service';
import { SnackService } from '@app/services/snack.service';
import { Observable } from 'rxjs';
import { User } from '@app/models/user';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public usersList$: Observable<User[]>;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private snackService: SnackService
  ) { }

  ngOnInit() {
    this.usersList$ = this.userService.getAllUsers();
  }

  public deleteUser(userId) {
    this.userService.deleteUser(userId).subscribe(result => {
      this.snackService.showBar("User deleted successfully!");
      this.usersList$ = this.userService.getAllUsers();
    }, error => {
      this.snackService.showBar("Error occured while deleting! Sorry!");
    });
  }

  public promoteToAdmin(userId) {
    //this.authService.promoteToAdmin(userId).subscribe(result => {
    //  this.snackService.showBar("User promoted successfully!");
    //  this.usersList$ = this.userService.getAllUsers();
    //}, error => {
    //  this.snackService.showBar("Error occured while promoting! Sorry!");
    //});
  }

  public degradeFromAdmin(userId) {
    //this.authService.degradeFromAdmin(userId).subscribe(result => {
    //  this.snackService.showBar("User degraded successfully!");
    //  this.usersList$ = this.userService.getAllUsers();
    //}, error => {
    //  this.snackService.showBar("Error occured while degrading! Sorry!");
    //});
  }

}
