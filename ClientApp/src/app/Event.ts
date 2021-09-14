export class Event { 
    id: number;
    eventStart: Date;
    eventEnd: Date;
    refundDate: Date;
    bandName: string;
    bandImagePath: string;
    bandLink: string;
    maxSeats: number;
    currentSeats: number;
    ticketPrice: number;
    description: string;
    googleCalendarID: string;
  }