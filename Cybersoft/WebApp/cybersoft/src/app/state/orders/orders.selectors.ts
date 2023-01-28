import { createSelector, createFeatureSelector } from '@ngrx/store';
import { of } from 'rxjs';
import { IOrdersState } from '../common/interfaces/orders.state';

const GetOrdersFromState = createFeatureSelector<IOrdersState>('orders');
// export const GetAuthFromState = (state: IAppState) => state.auth;

export const isOrdersLoadingSelector = createSelector(
  GetOrdersFromState,
  (state: IOrdersState) => state?.isLoading
);

export const ordersSelector = createSelector(
  GetOrdersFromState,
  (state: IOrdersState) => state?.orders
);

export const ordersErrorSelector = createSelector(
  GetOrdersFromState,
  (state: IOrdersState) => state?.error
);

export const ordersStatusSelector = createSelector(
  GetOrdersFromState,
  (state: IOrdersState) => state?.status
);
