import { User } from '@app/models/user';

export class Image {

  constructor(
    public id?: string,
    public author?: User,
    public mediaUrl?: string,
    public title?: string,
    public createdAt?: Date,
    public modifiedAt?: Date
  ){};
}
