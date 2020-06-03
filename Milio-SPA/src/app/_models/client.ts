import { User } from './user';

export interface Client extends User{
  numberOfChildren?: number;
}
