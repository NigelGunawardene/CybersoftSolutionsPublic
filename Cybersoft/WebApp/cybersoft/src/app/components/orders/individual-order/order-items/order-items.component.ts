import {Component, Input, OnInit} from '@angular/core';
import {Order, OrderDetails} from 'src/app/models/order/order';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.scss']
})
export class OrderItemsComponent implements OnInit {

  @Input() orderDetail!: OrderDetails;

  constructor() { }

  ngOnInit(): void {
  }

}
