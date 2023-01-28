import { createReducer, on } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as CartActions from './cart.actions';
import { Injectable } from '@angular/core';
import { of, from } from 'rxjs';
import { switchMap, map, catchError, withLatestFrom } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { StatusEnums } from '../common/enums/StatusEnums';
import { ICartState } from '../common/interfaces/cart.state';
import { EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { CartItem } from 'src/app/models/cart/cart-item';
import { cartAdapter } from './cart.adapter';

export const initialCartState: ICartState = cartAdapter.getInitialState({
  isLoading: false,
  error: null,
  status: StatusEnums.PENDING,
});

export const CartReducer = createReducer(
  initialCartState,

  on(CartActions.LoadCartCommand, (state) => ({
    ...state,
    isLoading: true,
    error: null,
    status: StatusEnums.LOADING,
  })),

  on(CartActions.AddToCartCommand, (state) => ({
    ...state,
    isLoading: true,
    error: null,
    status: StatusEnums.LOADING,
  })),

  on(CartActions.RemoveFromCartCommand, (state) => ({
    ...state,
    isLoading: true,
    error: null,
    status: StatusEnums.LOADING,
  })),

  on(CartActions.loadCartItems, (state, { cartItems }) => {
    return cartAdapter.setAll(cartItems, {
      ...state,
      isLoading: false,
      error: null,
      status: StatusEnums.SUCCESS,
    });
  }),

  // if no additional paramters, can do this. otherwise do above
  // on(CartActions.loadCartItems, (state, { cartItems }) => {
  //   return cartAdapter.setAll(cartItems, state);
  // }),

  on(CartActions.addCartItem, (state, { cartItem }) => {
    return cartAdapter.addOne(cartItem, state);
  }),
  on(CartActions.addCartItems, (state, { cartItems }) => {
    return cartAdapter.addMany(cartItems, state);
  }),
  on(CartActions.setCartItem, (state, { cartItem }) => {
    return cartAdapter.setOne(cartItem, state);
  }),
  on(CartActions.setCartItems, (state, { cartItems }) => {
    return cartAdapter.setMany(cartItems, state);
  }),
  on(CartActions.upsertCartItem, (state, { cartItem }) => {
    return cartAdapter.upsertOne(cartItem, {
      ...state,
      isLoading: false,
      error: null,
      status: StatusEnums.SUCCESS,
    });
  }),
  on(CartActions.upsertCartItems, (state, { cartItems }) => {
    return cartAdapter.upsertMany(cartItems, state);
  }),
  on(CartActions.updateCartItem, (state, { update }) => {
    return cartAdapter.updateOne(update, state);
  }),
  on(CartActions.updateCartItems, (state, { updates }) => {
    return cartAdapter.updateMany(updates, state);
  }),
  on(CartActions.mapCartItem, (state, { entityMap }) => {
    return cartAdapter.mapOne(entityMap, state);
  }),
  on(CartActions.mapCartItems, (state, { entityMap }) => {
    return cartAdapter.map(entityMap, state);
  }),
  on(CartActions.deleteCartItem, (state, { id }) => {
    return cartAdapter.removeOne(id, {
      ...state,
      isLoading: false,
      error: null,
      status: StatusEnums.SUCCESS,
    });
  }),
  on(CartActions.deleteCartItems, (state, { ids }) => {
    return cartAdapter.removeMany(ids, state);
  }),
  on(CartActions.deleteCartItemsByPredicate, (state, { predicate }) => {
    return cartAdapter.removeMany(predicate, state);
  }),
  on(CartActions.clearCartItems, (state) => {
    return cartAdapter.removeAll({ ...state, selectedUserId: null });
  }),
  on(CartActions.cartOperationFailure, (state) => {
    return cartAdapter.removeAll({ ...state, selectedUserId: null });
  }),

  on(CartActions.cartOperationFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    error: error,
    status: StatusEnums.ERROR,
  }))
);

export const getLoadingState = (state: ICartState) => state.isLoading;

// get the selectors
const { selectIds, selectEntities, selectAll, selectTotal } =
  cartAdapter.getSelectors();

// select the array of user ids
export const selectCartItemIds = selectIds;

// select the dictionary of user entities
export const selectCartItemEntities = selectEntities;

// select the array of users
export const selectAllCartItems = selectAll;

// select the total user count
export const selectCartItemsTotal = selectTotal;

/*
if we want to have a condition inside a reducer we can do something like -
  on(CartActions.loadCartItems, (state, { cartItem }) => {
    const existingCart = state.cart;
    if(!state.cart.some(existingProduct => existingProduct.id === cartItem.id)){
      existingCart = [...existingCart, cartItem]
    }
    return ({...state, cart: existingCart})
  }

  and to remove - 
  on(CartActions.loadCartItems, (state, { cartId }) => {
  const existingProducts = state.cart.filter((cartItem: CartItem) => cartItem.id != cartId)
*/

// on(CartActions.GetCart, (state) => ({
//   ...state,
//   isLoading: true,
//   error: null,
//   status: StatusEnums.LOADING,
// })),

// on(CartActions.loadCartItems, (state, { cartItems }) => {
//   return cartAdapter.setAll(cartItems, state);
// }),

// on(CartActions.GetCartFailure, (state, { error }) => ({
//   ...state,
//   isLoading: false,
//   cart: [],
//   error: error,
//   status: StatusEnums.ERROR,
// })),
// on(CartActions.AddToCart, (state) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: null,
//   status: StatusEnums.PENDING,
// })),
// on(CartActions.AddToCartSuccess, (state, { payload }) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: null,
//   status: StatusEnums.PENDING,
// })),
// on(CartActions.AddToCartFailure, (state, { error }) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: error,
//   status: StatusEnums.PENDING,
// })),
// on(CartActions.RemoveFromCart, (state) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: null,
//   status: StatusEnums.PENDING,
// })),
// on(CartActions.RemoveFromCartSuccess, (state) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: null,
//   status: StatusEnums.PENDING,
// })),
// on(CartActions.RemoveFromCartFailure, (state, { error }) => ({
//   ...state,
//   isLoading: false,
//   cart: state.entities,
//   error: error,
//   status: StatusEnums.PENDING,
// })),
