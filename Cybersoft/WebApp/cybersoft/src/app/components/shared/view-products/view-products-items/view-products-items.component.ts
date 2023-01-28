import {Component, Input, OnInit} from '@angular/core';
import {Product} from "../../../../models/product/product";
import {MessengerService} from "../../../../services/messenger/messenger.service";
import {CartService} from "../../../../services/cart/cart.service";
import {CartItem} from "../../../../models/cart/cart-item";

@Component({
  selector: 'app-view-products-items',
  templateUrl: './view-products-items.component.html',
  styleUrls: ['./view-products-items.component.scss']
})
export class ViewProductsItemsComponent implements OnInit {

  @Input() productItem!: Product;

  constructor(
    private messageService: MessengerService,
    private cartService: CartService
  ) { }

  ngOnInit(): void {

  }

}
