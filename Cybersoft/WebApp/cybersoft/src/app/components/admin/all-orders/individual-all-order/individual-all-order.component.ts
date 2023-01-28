import {Component, Input, OnInit} from '@angular/core';
import { OrderService } from 'src/app/services/order/order.service';
import {Order, OrderDetails} from 'src/app/models/order/order';
import {Product} from "src/app/models/product/product";
import {MessengerService} from "../../../../services/messenger/messenger.service";

@Component({
  selector: 'app-individual-all-order',
  templateUrl: './individual-all-order.component.html',
  styleUrls: ['./individual-all-order.component.scss']
})
export class IndividualAllOrderComponent implements OnInit {

  @Input() individualAllOrder!: Order;
  allOrderDetailList: OrderDetails[] = [];

  constructor(
    private orderService: OrderService,
    private messageService: MessengerService
  ) { }

  ngOnInit(): void {
    this.allOrderDetailList = this.individualAllOrder.orderDetails;
  }

  completeOrder(orderId: number){
    this.orderService.completeOrder(orderId).subscribe(status => {
      this.messageService.sendStatusMessage(status);
      },err => console.log(err),
    );
  }

}
