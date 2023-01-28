import { createAction, props } from '@ngrx/store';
import { CartItem } from 'src/app/models/cart/cart-item';
import { Order } from 'src/app/models/order/order';
import { Product } from 'src/app/models/product/product';
import { OrdersActionEnums } from './enums/OrdersActionsEnums';


export const GetOrders = createAction(
    OrdersActionEnums.GET_ORDERS,
);

export const GetOrdersSuccess = createAction(
    OrdersActionEnums.GET_ORDERS_SUCCESS,
    props<{ payload: Order[] }>() //, status: string
  );

  export const GetOrdersFailure = createAction(
    OrdersActionEnums.GET_ORDERS_FAILURE,
    props<{ error: string }>()
  );

//   export const AddToCart = createAction(
//     OrdersActionEnums.ADD_TO_CART,
//     props<{ product: Product }>()
//   );

//   export const RemoveFromCart = createAction(
//     OrdersActionEnums.REMOVE_FROM_CART,
//     props<{ cartId: number }>()
//   );