import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartItem } from 'src/app/models/cart/cart-item';
import { CartService } from 'src/app/services/cart/cart.service';
import { OrderService } from 'src/app/services/order/order.service';
import { Router } from '@angular/router';
import { MessengerService } from 'src/app/services/messenger/messenger.service';
import { Observable, Subscription } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import * as CartSelector from 'src/app/state/cart/cart.selectors';
import { Store } from '@ngrx/store';
import { StateService } from 'src/app/services/state/state.service';

declare var window: any;

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit, OnDestroy {
  constructor(
    //private messageService: MessengerService,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
    private store: Store,
    private stateService: StateService
  ) {}

  cartItems: CartItem[] = [];
  orderItems: number[] = [];
  cartTotal = 0;
  loading = false;

  cartItemsObservable$!: Observable<CartItem[]>;
  cartItemsSubscription$!: Subscription;

  formModal: any;

  ngOnInit(): void {
    //this.handleSubscription()
    this.formModal = new window.bootstrap.Modal(
      document.getElementById('orderPlacedModal')
    );
    this.cartItemsObservable$ = this.store.select(CartSelector.selectAllCartItems).pipe(
      tap((items) => {
        let tp: number = 0;
        items.forEach((x) => (tp = tp + x.price * x.quantity));
        this.cartTotal = tp;
      })
    );

  }

  // loadCartItems() {
  //   this.cartService.getCartItems().subscribe((items: CartItem[]) => {
  //     this.cartItems = items;
  //     this.caluclateCartTotal();
  //   })
  // }
  // loadCartItems() {
  //   this.cartItemsObservable$.pipe(
  //     tap((items) => {
  //       let tp: number = 0;
  //       items.forEach((x) => (tp = tp + x.price * x.quantity));
  //       this.cartTotal = tp;
  //     })
  //   );
  // }

  // caluclateCartTotal() {
  //   this.cartTotal = 0;
  //   this.cartItems.forEach(item => {
  //     this.cartTotal += (item.quantity * item.price)
  //   })
  // }

  placeOrder() {
    this.orderItems = [];
    this.changeButtonStatus();
    this.cartItemsSubscription$ = this.cartItemsObservable$.subscribe(
      (items) => {
        items.forEach((item) => this.orderItems.push(item.id!));
        //this.messageService.sendNumberOfCartItemsMessage(0);
        //this.router.navigate(['/orders']);
      }
    );

    this.orderService.placeOrder(this.orderItems).subscribe((order: any) => {
      this.stateService.updateCartAndOrders();
      this.changeButtonStatus();
      this.openModal();
    });

    // not used below method
    // this.cartItems.forEach((item) => {
    //   this.orderItems.push(item.id!);
    // });
  }

  // handleSubscription() {
  //   this.messageService.getCartItemIdMessage().subscribe((cartItemId: number) => {
  //     this.loadCartItems();
  //   })
  // }

  changeButtonStatus() {
    this.loading = !this.loading;
  }

  openModal() {
    this.formModal.show();
  }

  closeModal() {
    this.formModal.hide();
    this.router.navigate(['/orders']);
  }

  ngOnDestroy() {
    if (this.cartItemsSubscription$) {
      this.cartItemsSubscription$.unsubscribe();
    }
    // this.productMessageSubscription$.unsubscribe();
    // this.cartItemMessageSubscription$.unsubscribe();
  }
}
