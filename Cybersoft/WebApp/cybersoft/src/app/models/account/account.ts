import { Role } from './role';

export class Account {
    // id: string;
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
    phoneNumber: string;
    role: Role;
    jwtToken?: string;

  constructor(userName: string, firstName: string, lastName: string, fullName:string, email: string, phoneNumber: string, jwtToken: string) { //id: string,
    // this.id = id;
    this.userName = userName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.fullName = fullName;
    this.email = email;
    this.phoneNumber = phoneNumber;
    this.role = Role.User;
    this.jwtToken = jwtToken;
  }
}

