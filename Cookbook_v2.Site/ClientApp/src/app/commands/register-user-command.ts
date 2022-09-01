export interface RegisterUserCommand {
  name: string;
  username: string;
  password: string;
  repeatPassword: string;
}