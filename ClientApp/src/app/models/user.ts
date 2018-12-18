import { Post } from './post';

export class User {

  constructor(
    public id: string,
    public email: string,
    public name: string,
    public surname: string,
    public friends: User[],
    public posts: Post[]
  ){}

}
