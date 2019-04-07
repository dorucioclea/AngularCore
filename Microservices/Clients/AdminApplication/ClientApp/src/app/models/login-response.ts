import { User } from './user';

export class LoginResponse {

  constructor(
    public user: User,
    public jwtToken: string,
    public expiresIn: string
  ){}

}
