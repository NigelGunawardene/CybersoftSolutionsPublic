import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order/order.service';
import { Order, OrderDetails } from 'src/app/models/order/order';
import { Product } from '../../../models/product/product';
import { fadeInAnimation } from '../../../route-animations';

@Component({
  selector: 'app-individual-order',
  templateUrl: './individual-order.component.html',
  styleUrls: ['./individual-order.component.scss'],
  animations: [fadeInAnimation],
})
export class IndividualOrderComponent implements OnInit {
  @Input() individualOrder!: Order;
  orderDetailList: OrderDetails[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderDetailList = this.individualOrder.orderDetails;
    // this.orderService
    //   .getOrderDetailsForOrder(this.individualOrder.id)
    //   .subscribe((orderDetails) => {
    //     this.orderDetailList = orderDetails;
    //   });
  }
}
