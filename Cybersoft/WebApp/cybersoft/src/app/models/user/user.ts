export class User {

  username: string;
  firstName: string;
  lastName: string;
  email: string;
  phonenumber: string;
  password: string;

  constructor(username: string, firstName: string, lastName: string, email: string, phonenumber: string, password: string) { //userId: number, productId: number, productName: string, quantity: number, price: number
    this.username = username;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.phonenumber = phonenumber;
    this.password = password;
  }
}

