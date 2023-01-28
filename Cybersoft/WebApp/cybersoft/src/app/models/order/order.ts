import {Product} from "src/app/models/product/product";

export class Order {
  id: number;
  userId: number;
  totalPrice: number;
  publicOrderNumber: string;
  message: number;
  isComplete: boolean;
  orderDate: string;
  orderDetails: OrderDetails[] = [];

  constructor(id: number, userId: number, totalPrice: number, publicOrderNumber: string, message: number, isComplete: boolean, orderDate: string) {
    this.id =id;
    this.userId = userId;
    this.totalPrice = totalPrice;
    this.publicOrderNumber = publicOrderNumber;
    this.message = message;
    this.isComplete = isComplete;
    this.orderDate = orderDate;
  }
}

export class OrderDetails {
  id: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  totalPrice: number;
  product: Product;

  constructor(id: number, orderId: number, productId: number, quantity: number, price: number, totalPrice: number, product: Product) {
    this.id =id;
    this.orderId = orderId;
    this.productId = productId;
    this.quantity = quantity;
    this.price =price
    this.totalPrice = totalPrice;
    this.product = product;
  }
}
