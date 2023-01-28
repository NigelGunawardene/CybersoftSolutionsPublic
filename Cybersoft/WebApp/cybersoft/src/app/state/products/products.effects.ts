import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { Product } from 'src/app/models/product/product';
import { ProductService } from 'src/app/services/product/product.service';
import * as ProductsActions from './products.actions';

@Injectable()
export class ProductsEffects {
  getProducts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductsActions.GetProducts),
      mergeMap(() => {
        return this.productsService.getProducts().pipe(
          map((products: Product[]) =>
            ProductsActions.GetProductsSuccess({ payload: products })
          ),
          catchError((error) =>
            of(ProductsActions.GetProductsFailure({ error: error }))
          )
        );
      })
    )
  );

  constructor(
    private actions$: Actions,
    private productsService: ProductService
  ) {}
}