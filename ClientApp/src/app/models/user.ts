import { FriendUser } from './friend-user';
export class User {

  constructor(
    public id: string,
    public email: string,
    public name: string,
    public surname: string,
    public friends: FriendUser[]
  ){}

  public get fullName() : string {
    return this.name + " " + this.surname;
  }

}
