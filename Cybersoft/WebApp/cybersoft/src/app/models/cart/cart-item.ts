export class CartItem {
  id: number | undefined;
  userId: number;
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  addedDate : Date

  constructor(id: number, userId: number, productId: number, productName: string, quantity: number, price: number, addedDate: Date) {
    this.userId = userId;
    this.productId = productId;
    this.productName = productName;
    this.quantity = quantity;
    this.price = price;
    this.addedDate = addedDate;
  }
}


