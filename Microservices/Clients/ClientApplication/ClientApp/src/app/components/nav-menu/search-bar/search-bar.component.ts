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

  
  public searchPhrase = new FormControl();
  public foundUsers$ = new Observable<User[]>();

  constructor(private searchService: SearchService) { }

  ngOnInit() {
    this.searchPhrase.valueChanges.subscribe(value => {
      if (value != '') {
        this.foundUsers$ = this.searchService.searchForUsers(value);
      } else {
        this.foundUsers$ = new Observable<User[]>();
      }
    }); 
  }
}
