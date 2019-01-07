import { User } from './user';

export class Post {

  constructor(
    public id: string,
    public author: User,
    public wallOwner: User,
    public content: string,
    public modifiedAt: Date,
    public createdAt: Date
  ){}

}
