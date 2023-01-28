export class UpsertProduct {
    id: number;
    name: string;
    description: string;
    price: number;
  
    constructor(id: number, name: string, description: string, price: number, imageUrl: string) {
      this.id = id;
      this.name = name;
      this.description = description;
      this.price = price;
    }
  }