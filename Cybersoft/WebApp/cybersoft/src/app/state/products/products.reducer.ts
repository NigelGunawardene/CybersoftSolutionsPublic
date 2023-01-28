import { createReducer, on } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as ProductsActions from './products.actions';
import { Injectable } from '@angular/core';
import { of, from } from 'rxjs';
import { switchMap, map, catchError, withLatestFrom } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { StatusEnums } from '../common/enums/StatusEnums';
import { IProductsState } from '../common/interfaces/products.state';

const initialProductsState: IProductsState = {
  isLoading: false,
  products: [],
  error: null,
  status: StatusEnums.PENDING,
};

export const ProductsReducer = createReducer(
  initialProductsState,
  on(ProductsActions.GetProducts, (state) => ({
    ...state,
    isLoading: true,
    products: [],
    error: null,
    status: StatusEnums.LOADING,
  })),
  on(ProductsActions.GetProductsSuccess, (state, { payload }) => ({
    ...state,
    isLoading: false,
    products: payload,
    error: null,
    status: StatusEnums.SUCCESS,
  })),
    on(ProductsActions.GetProductsFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    products: [],
    error: error,
    status: StatusEnums.ERROR,
  })),
);



