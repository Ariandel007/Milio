export interface Appointment {
  Acepted: boolean;
  id: number;
  start: Date;
  end: Date;
  cost: number;
  carerId: number;
  clientId: number;
}
