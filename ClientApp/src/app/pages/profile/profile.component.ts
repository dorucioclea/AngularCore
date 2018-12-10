import { UserService } from './../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { User } from '../../models/user';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  profile$: Observable<User>;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.profile$ = this.route.paramMap.pipe(
      switchMap( params => {
        return this.userService.getUser(params.get('id'));
      })
    )
  }

}
