import { Component, OnDestroy, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product/product';
import { MessengerService } from 'src/app/services/messenger/messenger.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { CartItem } from '../../../models/cart/cart-item';
import { Observable, Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import * as CartSelector from 'src/app/state/cart/cart.selectors';
import {map, tap} from 'rxjs/operators';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit, OnDestroy {
  cartItems: CartItem[] = [];
  cartTotal = 0;

  // productMessageSubscription$!: Subscription;
  // cartItemMessageSubscription$!: Subscription;
  cartItemsSubscription$!: Observable<CartItem[]>;

  constructor(
    private messageService: MessengerService,
    private cartService: CartService,
    private store: Store
  ) {}

  ngOnInit(): void {
    //this.handleSubscription();
    this.loadCartItems();
  }

  // handleSubscription() {
  //   this.productMessageSubscription$ = this.messageService
  //     .getProductMessage()
  //     .subscribe((product: Product) => {
  //       this.loadCartItems();
  //     });

  //   this.cartItemMessageSubscription$ = this.messageService
  //     .getCartItemIdMessage()
  //     .subscribe((cartItemId: number) => {
  //       this.loadCartItems();
  //     });
  // }

  loadCartItems() {
    this.cartItemsSubscription$ = this.store.select(CartSelector.selectAllCartItems).pipe(
      tap(items => 
        {
          let tp: number = 0;
          items.forEach(x => tp = tp + x.price * x.quantity);
          this.cartTotal = tp;
      })
    )
    
    // this.cartItemsSubscription$ = this.cartService
    //   .getCartItems()
    //   .subscribe((items: CartItem[]) => {
    //     this.cartItems = items;
    //     this.caluclateCartTotal();
    //     this.messageService.sendNumberOfCartItemsMessage(this.cartItems.length);
    //   });
  }

  // caluclateCartTotal() {
  //   this.cartTotal = 0;
  //   this.cartItems.forEach((item) => {
  //     this.cartTotal += item.quantity * item.price;
  //   });
  // }

  ngOnDestroy() {
    // this.productMessageSubscription$.unsubscribe();
    // this.cartItemMessageSubscription$.unsubscribe();
  }
}
