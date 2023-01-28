import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order/order.service';
import { Order } from 'src/app/models/order/order';
import {longFadeInAnimation} from "../../route-animations";
import { Store } from '@ngrx/store';
// import {Product} from "../../models/product/product";
// import {ProductService} from "../../services/product/product.service";
import * as OrdersSelector from 'src/app/state/orders/orders.selectors';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
  animations: [
    longFadeInAnimation
  ]
})
export class OrdersComponent implements OnInit, OnDestroy {

  orderList: Order[] = [];

  ordersSubscription$!: Subscription;

  constructor(
    private orderService: OrderService,
    private store: Store,
  ) { }

  ngOnInit(): void {
    this.ordersSubscription$ = this.store.select(OrdersSelector.ordersSelector).subscribe(orders => {
      this.orderList = orders;
      // this.formatDate();
    });
    
    // this.orderService.getOrdersForUser()
    //   .subscribe(orders => {
    //     this.orderList = orders;
    //     this.orderList.forEach(function (order) {
    //       var array = order.orderDate.split('T');
    //       //var time = array[1].substring(0, array[1].length-6);
    //       order.orderDate = array[0];// + "  " + time;
    //     });
    //   });
  }

  formatDate(){
    this.orderList.forEach(function (order) {
      //console.log(Object.getOwnPropertyDescriptor(order, 'orderDate'));
      var array = order.orderDate.split('T');
      //var time = array[1].substring(0, array[1].length-6);
      order.orderDate = array[0];
    });
  }

  ngOnDestroy(): void {
      if(this.ordersSubscription$){
        this.ordersSubscription$.unsubscribe();
      }
  }

}
