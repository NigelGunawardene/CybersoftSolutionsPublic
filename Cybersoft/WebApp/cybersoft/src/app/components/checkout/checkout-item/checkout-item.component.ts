import {Component, Input, OnInit} from '@angular/core';
import { CartService } from 'src/app/services/cart/cart.service';
import { CartItem } from 'src/app/models/cart/cart-item';
import {MessengerService} from "../../../services/messenger/messenger.service";
import { Store } from '@ngrx/store';
import * as CartActions from 'src/app/state/cart/cart.actions';

@Component({
  selector: 'app-checkout-item',
  templateUrl: './checkout-item.component.html',
  styleUrls: ['./checkout-item.component.scss']
})
export class CheckoutItemComponent implements OnInit {

  @Input() cartItem!: CartItem;
  loading = false;

  constructor(
    //private messageService: MessengerService,
    private cartService: CartService,
    private store: Store,
  ) { }

  ngOnInit(): void {
  }

  removeCartItem(cartItemId: number) {
    this.changeButtonStatus();
    this.store.dispatch(CartActions.RemoveFromCartCommand({ id : cartItemId }));
    // this.cartService.deleteProductFromCart(cartItemId).subscribe(() => {
    //   this.messageService.sendCartItemIdMessage(cartItemId);
    //   this.changeButtonStatus();
    // })
  }

  changeButtonStatus(){
    this.loading = !this.loading;
  }

}
