import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {CartItem} from "../../models/cart/cart-item";
import {Order, OrderDetails} from "../../models/order/order";
import {MessengerService} from "../messenger/messenger.service";
import {PagedCollection} from "../../models/paged-collection/paged-collection";
import {Account} from "../../models/account";
import {catchError} from "rxjs/operators";


@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly apiPath: string = 'api/order';
  private readonly orderDetailsapiPath: string = 'api/orderdetail/';

  constructor(
    private http: HttpClient,
    private messageService: MessengerService,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  placeOrder(cartItems: number[]): Observable<any> {
    return this.http.post(`${this.baseUrl}${this.apiPath}`, cartItems); //{ cartItem }
  }
  getOrdersForUser(): Observable<Order[]> { // Observable<any>
    return this.http.get<Order[]>(`${this.baseUrl}${this.apiPath}/user`);
  }
  getAllActiveOrders(): Observable<Order[]> { // Observable<any>
    return this.http.get<Order[]>(`${this.baseUrl}${this.apiPath}/active`);
  }
  getAllOrders(): Observable<Order[]> { // Observable<any>
    return this.http.get<Order[]>(`${this.baseUrl}${this.apiPath}/all`);
  }
  getOrderDetailsForOrder(orderId: number): Observable<OrderDetails[]> { // Observable<any>
    return this.http.get<OrderDetails[]>(`${this.baseUrl}${this.orderDetailsapiPath}${orderId}`);
  }
  completeOrder(orderId: number){
    return this.http.get<Boolean>(`${this.baseUrl}${this.apiPath}/complete/${orderId}`);
  }
  getOrdersPaginated(orderstatus: string, pagesize: number, pagenumber: number) {
    return this.http.get<PagedCollection<Order>>(`${this.baseUrl}${this.apiPath}/paginated/?orderstatus=${orderstatus}&pagesize=${pagesize}&pagenumber=${pagenumber}`)
      .pipe(catchError(this.handleError<PagedCollection<Order>>()));
    // return this.http.get<Account[]>(`${this.baseUrl}${this.apiPath}/all/${accountType}/${pagesize}/${pagenumber}`);
  }
  private handleError<T>(operation = 'operation', result? :T){
    return (error: any): Observable<T> =>{
      return of(result as T);
    }
  }

}
