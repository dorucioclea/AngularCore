import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '@app/models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private searchUrl = "/api/v1/search/";

  constructor(private http: HttpClient) { }

  public searchForUsers(phrase: string){
    return this.http.get<User[]>(this.searchUrl + phrase);
  }
}
