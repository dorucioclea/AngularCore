import { Post } from '@app/models/post';
import { Image } from '@app/models/image';
import { Comment } from '@app/models/comment';

export class User {

  constructor(
    public id: string,
    public email: string,
    public password: string,
    public name: string,
    public surname: string,
    public isAdmin: boolean,
    public profilePicture: Image,
    public images: Image[],
    public comments: Comment[],
    public friends: User[],
    public posts: Post[],
    public createdAt: Date,
    public modifiedAt: Date
  ){}

}
