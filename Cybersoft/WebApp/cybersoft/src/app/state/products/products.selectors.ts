import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IProductsState } from '../common/interfaces/products.state';


const GetProductsFromState = createFeatureSelector<IProductsState>('products');
// export const GetProductsFromState = (state: IAppState) => state.products;


export const isProductsLoadingSelector = createSelector(
  GetProductsFromState,
  (state: IProductsState) => state.isLoading
);

export const hasProductsLoadedSelector = createSelector(
  GetProductsFromState,
  (state: IProductsState) => state.status
);

export const productsSelector = createSelector(
  GetProductsFromState,
  (state: IProductsState) => state?.products
);

export const productsErrorSelector = createSelector(
  GetProductsFromState,
  (state: IProductsState) => state?.error
);