import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { User } from '@app/models/user';
import { Image } from '@app/models/image';
import { Observable } from 'rxjs';
import { SearchService } from '@app/services/search.service';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  private defaultProfileSrc = "../../../assets/images/default-profile-pic.png";

  public searchPhrase = new FormControl();
  public foundUsers$ = new Observable<User[]>();

  constructor(private searchService: SearchService) { }

  ngOnInit() {
    this.searchPhrase.valueChanges.subscribe(value => {
      if (value != '') {
        this.foundUsers$ = this.searchService.searchForUsers(value).pipe(
          map(users => {
            return this.setDefaultProfilePictures(users);
          })
        );
      } else {
        this.foundUsers$ = new Observable<User[]>();
      }
    }); 
  }

  private setDefaultProfilePictures(users) {
    if (!users) {
      return;
    }

    users.forEach(friend => {
      if (!friend.profilePicture) {
        let noProfile = new Image();
        noProfile.mediaUrl = this.defaultProfileSrc;
        friend.profilePicture = noProfile;
      }
    })
    return users;
  }
}
