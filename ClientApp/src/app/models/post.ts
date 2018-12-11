import { User } from './user';

export class Post {

  constructor(
    public id: string,
    public owner: User,
    public content: string,
    public modifiedAt: Date,
    public createdAt: Date
  ){}

}
