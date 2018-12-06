export class User {

  constructor(
    public id: string,
    public name: string,
    public surname: string,
    public email: string,
    public password: string,
    public modifiedAt: Date,
    public createdAt: Date
  ){}

}
