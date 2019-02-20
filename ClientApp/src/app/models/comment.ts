import { User } from '@app/models/user';
import { Post } from '@app/models/post';

export class Comment {

  constructor(
    public id: string,
    public author: User,
    public post: Post,
    public content: string,
    public createdAt: Date,
    public modifiedAt: Date
  ){}
}
