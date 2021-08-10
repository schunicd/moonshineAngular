export class Event { 
    id: number;
    title: string;
    eventStart: Date;
    eventEnd: Date;
    refundDate: Date;
    bandId: number;
    maxSeats: number;
    currentSeats: number;
    ticketPrice: number;
    googleCalendarID: string;
  }