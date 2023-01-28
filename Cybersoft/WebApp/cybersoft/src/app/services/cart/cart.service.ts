import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CartItem } from 'src/app/models/cart/cart-item';
import { Product } from 'src/app/models//product/product';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private readonly apiPath: string = 'api/cart';
  private readonly deleteApiPath: string = '/removecartitem/'

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  getCartItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.baseUrl}${this.apiPath}`);
    // return this.http.get<CartItem[]>(`${this.baseUrl}${this.apiPath}?userId=${userId}`);

    //return this.http.get<CartItem[]>(`${this.baseUrl}${this.apiPath}?userId=${userId}`).pipe(
    //  map((result: any[]) => {
    //    let cartItems: CartItem[] = [];

    //    for(let item of result){
    //      let productExists = false;

    //      for (let i in cartItems) {
    //        if (cartItems[i].productId === item.product.id) {
    //          cartItems[i].quantity++
    //          productExists = true;
    //          break;
    //        }
    //      }

    //      if (!productExists) {
    //        cartItems.push(new CartItem(item.id, item.product.id, item.name, item.quantity, item.price));
    //        //  this.cartItems.push({
    //        //  id: product.id,
    //        //  name: product.name,
    //        //  description: product.description,
    //        //  price: product.price,
    //        //  quantity: 1,
    //        //  imageUrl: product.imageUrl,
    //        //  dateAdded: product.dateAdded,
    //        //})
    //      }
    //    }
    //    return cartItems;
    //  })
    //);
  }

  addProductToCart(cartItem: CartItem): Observable<any> {
    return this.http.post(`${this.baseUrl}${this.apiPath}`, cartItem);
  }

  deleteProductFromCart(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${this.apiPath}${this.deleteApiPath}${cartItemId}`);
  }
}
