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
    private userService: UserService,
  ) { }

  ngOnInit() {
    // this.profile$ = this.route.paramMap.pipe(
    //   switchMap( params => {
    //     return this.userService.getUser(params.get('id'));
    //   })
    // );
    this.profile$ = this.userService.getUser("dd53af61-656f-45ce-8d3d-c6ff7c501985");
    this.profile$.subscribe( user => {
      console.log("Profile obtained: ", user);
    })
  }

}
