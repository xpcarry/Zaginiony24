export interface IUser {
    name:string;
    username: string;
    email: string;
    isAdmin: boolean;
    accessToken: string;
}

export interface IUserFormValues{
    email:string;
    username:string;
    password:string;
    name:string;
    surname:string;
    phoneNumber:string;
}

export interface IUserSettings{
    id:string;
    username:string;
    email:string;
    canLogIn:boolean;
    noticesListedCount:number;
}
