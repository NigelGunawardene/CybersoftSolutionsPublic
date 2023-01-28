import { createAction, props } from '@ngrx/store';
import { CartItem } from 'src/app/models/cart/cart-item';
import { Product } from 'src/app/models/product/product';
import { CartActionEnums } from './enums/CartActionEnums';
import { Update, EntityMap, EntityMapOne, Predicate } from '@ngrx/entity';

// COMMANDS
export const LoadCartCommand = createAction(
  CartActionEnums.LOAD_CART_ITEMS_COMMAND
);
export const AddToCartCommand = createAction(
  CartActionEnums.ADD_TO_CART_COMMAND,
  props<{ cartItem: CartItem }>()
);
export const RemoveFromCartCommand = createAction(
  CartActionEnums.DELETE_FROM_CART_COMMAND,
  props<{ id : number }>()
);

// ERRORS
export const cartOperationFailure = createAction(
  CartActionEnums.CART_OPERATION_FAILURE,
  props<{ error: string }>()
);

// ACTIONS
export const loadCartItems = createAction(
  CartActionEnums.LOAD_CART_ITEMS,
  props<{ cartItems: CartItem[] }>()
);
export const addCartItem = createAction(
  CartActionEnums.ADD_CART_ITEM,
  props<{ cartItem: CartItem }>()
);
export const addCartItems = createAction(
  CartActionEnums.ADD_CART_ITEMS,
  props<{ cartItems: CartItem[] }>()
);
export const setCartItem = createAction(
  CartActionEnums.SET_CART_ITEM,
  props<{ cartItem: CartItem }>()
);
export const setCartItems = createAction(
  CartActionEnums.SET_CART_ITEMS,
  props<{ cartItems: CartItem[] }>()
);
export const upsertCartItem = createAction(
  CartActionEnums.UPSERT_CART_ITEM,
  props<{ cartItem: CartItem }>()
);
export const upsertCartItems = createAction(
  CartActionEnums.UPSERT_CART_ITEMS,
  props<{ cartItems: CartItem[] }>()
);
export const updateCartItem = createAction(
  CartActionEnums.UPDATE_CART_ITEM,
  props<{ update: Update<CartItem> }>()
);
export const updateCartItems = createAction(
  CartActionEnums.UPDATE_CART_ITEMS,
  props<{ updates: Update<CartItem>[] }>()
);
export const mapCartItem = createAction(
  CartActionEnums.MAP_CART_ITEM,
  props<{ entityMap: EntityMapOne<CartItem> }>()
);
export const mapCartItems = createAction(
  CartActionEnums.MAP_CART_ITEMS,
  props<{ entityMap: EntityMap<CartItem> }>()
);
export const deleteCartItem = createAction(
  CartActionEnums.DELETE_CART_ITEM,
  props<{ id: number }>()
);
export const deleteCartItems = createAction(
  CartActionEnums.DELETE_CART_ITEMS,
  props<{ ids: number[] }>()
);
export const deleteCartItemsByPredicate = createAction(
  CartActionEnums.DELETE_CART_ITEMS_BY_PREDICATE,
  props<{ predicate: Predicate<CartItem> }>()
);
export const clearCartItems = createAction(CartActionEnums.CLEAR_CART_ITEMS);

// export const GetCartSuccess = createAction(
//     CartActionEnums.GET_CART_SUCCESS,
//     props<{ payload: CartItem[] }>() //, status: string
//   );

//   export const GetCartFailure = createAction(
//     CartActionEnums.GET_CART_FAILURE,
//     props<{ error: string }>()
//   );

//   export const AddToCart = createAction(
//     CartActionEnums.ADD_TO_CART,
//     props<{ payload: CartItem }>()
//   );

//   export const AddToCartSuccess = createAction(
//     CartActionEnums.ADD_TO_CART_SUCCESS,
//     props<{ payload: CartItem }>()
//   );

//   export const AddToCartFailure = createAction(
//     CartActionEnums.ADD_TO_CART_FAILURE,
//     props<{ error: string }>()
//   );

//   export const RemoveFromCart = createAction(
//     CartActionEnums.REMOVE_FROM_CART,
//     props<{ payload: number }>()
//   );

//   export const RemoveFromCartSuccess = createAction(
//     CartActionEnums.REMOVE_FROM_CART_SUCCESS,
//     props<{ payload: number }>()
//   );

//   export const RemoveFromCartFailure = createAction(
//     CartActionEnums.REMOVE_FROM_CART_FAILURE,
//     props<{ error: string }>()
//   );
