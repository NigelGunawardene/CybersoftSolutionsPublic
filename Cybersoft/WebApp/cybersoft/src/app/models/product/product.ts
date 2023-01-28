export class Product {
  id: number;
  name: string;
  description: string;
  price: number;
  quantity: number;
  imageUrl: string;
  dateAdded: Date;
  isDeleted: boolean;

  constructor(id: number, name: string, description: string, price: number, imageUrl: string, dateAdded: Date) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.price = price;
    this.quantity = 1;
    this.imageUrl = imageUrl;
    this.dateAdded = dateAdded;
    this.isDeleted = false;
  }
}
