export interface User {
  id: number;
  userName: string;
  name: string;
  lastName: number;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  aboutMe?: string;
  photos?: Photo[];
}
