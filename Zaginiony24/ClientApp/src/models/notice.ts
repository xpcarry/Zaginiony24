import { IUser } from "./user";

export interface INotice {
    id: string;
    name: string;
    surname: string;
    gender: string;
    city: string;
    district: string;
    dateOfDisappearance: Date | null;
    lastSeenPlace: string;
    age: number; 
    height: number;
    eyeColor: string;
    specialCharacters: string;
    description: string;
    datePosted: Date | null;
    user: IUser;
}

export interface IManageNotice {
    id: string;
    name: string;
    surname: string;
    datePosted: Date | null;
    username:string;
}