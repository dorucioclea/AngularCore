import { LoggedUser } from './logged-user';

export class LoginResponse {

  constructor(
    public user: LoggedUser,
    public jwtToken: string,
    public expiresIn: string
  ){}

}
