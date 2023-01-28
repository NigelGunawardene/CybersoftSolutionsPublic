import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { Order } from 'src/app/models/order/order';
import { OrderService } from 'src/app/services/order/order.service';
import * as OrdersActions from './orders.actions';

@Injectable()
export class OrdersEffects {
  getOrders$ = createEffect(() =>
    this.actions$.pipe(
      ofType(OrdersActions.GetOrders),
      mergeMap(() => {
        return this.orderService.getOrdersForUser().pipe(
          map((orders: Order[]) => OrdersActions.GetOrdersSuccess({ payload: orders })),
          catchError((error) => of(OrdersActions.GetOrdersFailure({ error: error })))
        );
      })
    )
  );

  constructor(
    private actions$: Actions,
    private orderService: OrderService
  ) {}
}