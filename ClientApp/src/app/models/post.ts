export class Post {

  constructor(
    public id: string,
    public ownerId: string,
    public content: string,
    public modifiedAt: Date,
    public createdAt: Date
  ){}

}
