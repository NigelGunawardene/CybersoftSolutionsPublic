import { Component, OnInit, Input } from '@angular/core';
import { CartService } from 'src/app/services/cart/cart.service';
import { MessengerService } from 'src/app/services/messenger/messenger.service';
import { CartItem } from 'src/app/models/cart/cart-item';
import { fadeOut, flyInOut } from 'src/app/route-animations';
import { Store } from '@ngrx/store';
import * as CartActions from 'src/app/state/cart/cart.actions';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.scss'],
  animations: [fadeOut, flyInOut],
})
export class CartItemComponent implements OnInit {
  @Input() cartItem!: CartItem;
  loading = false;

  constructor(
    private messageService: MessengerService,
    private cartService: CartService,
    private store: Store
  ) {}

  ngOnInit(): void {}

  removeCartItem(cartItemId: number) {
    this.changeButtonStatus();
    this.store.dispatch(CartActions.RemoveFromCartCommand({ id : cartItemId }));
    // this.cartService.deleteProductFromCart(cartItemId).subscribe(() => {
    //   this.messageService.sendCartItemIdMessage(cartItemId);
    // })
  }

  changeButtonStatus() {
    this.loading = !this.loading;
  }
}
