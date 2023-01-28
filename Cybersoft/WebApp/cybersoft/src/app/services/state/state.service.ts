import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import * as ProductActions from 'src/app/state/products/products.actions';
import * as AuthActions from 'src/app/state/auth/auth.actions';
import * as CartActions from 'src/app/state/cart/cart.actions';
import * as OrdersActions from 'src/app/state/orders/orders.actions';

@Injectable({
    providedIn: 'root',
  })
  export class StateService {

    constructor(private store: Store){

    }

    updateCartAndOrders(){
        this.store.dispatch(CartActions.LoadCartCommand());
        this.store.dispatch(OrdersActions.GetOrders());
    }

    updateProducts(){
      this.store.dispatch(ProductActions.GetProducts());
    }

  }