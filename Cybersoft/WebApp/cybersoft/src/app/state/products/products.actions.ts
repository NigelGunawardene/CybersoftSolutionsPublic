import { createAction, props } from '@ngrx/store';
import { Product } from 'src/app/models/product/product';
import { ProductsActionEnums } from './enums/ProductsActionEnums';

export const GetProducts = createAction(
  ProductsActionEnums.GET_PRODUCTS,
);

export const GetProductsSuccess = createAction(
    ProductsActionEnums.GET_PRODUCTS_SUCCESS,
    props<{ payload: Product[] }>() //, status: string
  );

  export const GetProductsFailure = createAction(
    ProductsActionEnums.GET_PRODUCTS_FAILURE,
    props<{ error: string }>()
  );
