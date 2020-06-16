import { User } from './user';

export interface Carer extends User{
  fareForHour?: number;
}
