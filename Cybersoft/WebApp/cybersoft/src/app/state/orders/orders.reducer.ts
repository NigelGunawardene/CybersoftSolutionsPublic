import { createReducer, on } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as OrdersActions from './orders.actions';
import { Injectable } from '@angular/core';
import { of, from } from 'rxjs';
import { switchMap, map, catchError, withLatestFrom } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { StatusEnums } from '../common/enums/StatusEnums';
import { IOrdersState } from '../common/interfaces/orders.state';



const initialOrdersState: IOrdersState = {
  isLoading: false,
  orders: [],
  error: null,
  status: StatusEnums.PENDING,
};

export const OrdersReducer = createReducer(
    initialOrdersState,
  on(OrdersActions.GetOrders, (state) => ({
    ...state,
    isLoading: true,
    orders: [],
    error: null,
    status: StatusEnums.LOADING,
  })),
  on(OrdersActions.GetOrdersSuccess, (state, { payload }) => ({
    ...state,
    isLoading: false,
    orders: payload,
    error: null,
    status: StatusEnums.SUCCESS,
  })),
    on(OrdersActions.GetOrdersFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    orders: [],
    error: error,
    status: StatusEnums.ERROR,
  })),
//     on(OrdersActions., (state) => ({
//     ...state,
//     isLoading: false,
//     orders: [],
//     error: null,
//     status: StatusEnums.PENDING,
//   })),
);