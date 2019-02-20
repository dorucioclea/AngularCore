import { User } from '@app/models/user';
import { Comment } from '@app/models/comment';

export class Post {

  constructor(
    public id: string,
    public author: User,
    public wallOwner: User,
    public comments: Comment[],
    public content: string,
    public modifiedAt: Date,
    public createdAt: Date
  ){}

}
