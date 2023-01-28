import { ActionReducerMap } from "@ngrx/store";
import { AuthEffects } from "./auth/auth.effects";
import { AuthReducer } from "./auth/auth.reducer";
import { CartEffects } from "./cart/cart.effects";
import { CartReducer } from "./cart/cart.reducer";
import { IAuthState } from "./common/interfaces/auth.state";
import { ICartState } from "./common/interfaces/cart.state";
import { IOrdersState } from "./common/interfaces/orders.state";
import { IProductsState } from "./common/interfaces/products.state";
import { OrdersEffects } from "./orders/orders.effects";
import { OrdersReducer } from "./orders/orders.reducer";
import { ProductsEffects } from "./products/products.effects";
import { ProductsReducer } from "./products/products.reducer";

export interface IAppState {
  auth: IAuthState,
  products: IProductsState,
  orders: IOrdersState,
  cart: ICartState,
}

export const AppReducers: ActionReducerMap<IAppState> = {
  auth: AuthReducer,
  products: ProductsReducer,
  cart: CartReducer,
  orders: OrdersReducer,
};

export const AppEffects = 
  [
    AuthEffects,
    ProductsEffects,
    CartEffects,
    OrdersEffects,
  ]
