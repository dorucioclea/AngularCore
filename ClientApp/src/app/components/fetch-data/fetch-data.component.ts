import { User } from './../../models/user';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  users: User[];
  authTest = false;
  authErrorText: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

  public testAuthEndpoint(){
    this.authTest = true;
    this.http.get<User[]>('/api/User/GetAllUsers').toPromise()
    .then( response => {
      this.users = response;
    }).catch( error => {
      this.authErrorText = error.message;
    })
  }

}
