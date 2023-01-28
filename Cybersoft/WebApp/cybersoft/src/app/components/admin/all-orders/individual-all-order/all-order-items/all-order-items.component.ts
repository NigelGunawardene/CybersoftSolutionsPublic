import {Component, Input, OnInit} from '@angular/core';
import {Order, OrderDetails} from 'src/app/models/order/order';

@Component({
  selector: 'app-all-order-items',
  templateUrl: './all-order-items.component.html',
  styleUrls: ['./all-order-items.component.scss']
})
export class AllOrderItemsComponent implements OnInit {

  @Input() orderDetail!: OrderDetails;

  constructor() { }

  ngOnInit(): void {
  }

}
