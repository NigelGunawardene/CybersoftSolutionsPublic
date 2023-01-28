import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { CartItem } from 'src/app/models/cart/cart-item';
import { CartService } from 'src/app/services/cart/cart.service';
import * as CartActions from './cart.actions';

@Injectable()
export class CartEffects {
  getCart$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CartActions.LoadCartCommand),
      mergeMap(() => {
        return this.cartService.getCartItems().pipe(
          map((cartItems: CartItem[]) =>
            CartActions.loadCartItems({ cartItems: cartItems })
          ),
          catchError((error) =>
            of(CartActions.cartOperationFailure({ error: error }))
          )
        );
      })
    )
  );

  addToCart$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CartActions.AddToCartCommand),
      mergeMap((action) => {
        return this.cartService.addProductToCart(action.cartItem).pipe(
          map(
            (cartItem: CartItem) => CartActions.upsertCartItem({cartItem : cartItem}),
          ),
          catchError((error) =>
            of(CartActions.cartOperationFailure({ error: error }))
          )
        );
      })
    )
  );

  deleteFromCart$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CartActions.RemoveFromCartCommand),
      mergeMap((action) => {
        return this.cartService.deleteProductFromCart(action.id).pipe(
          map(() => CartActions.deleteCartItem({id : action.id})),
          catchError((error) =>
            of(CartActions.cartOperationFailure({ error: error }))
          )
        );
      })
    )
  );
  constructor(private actions$: Actions, private cartService: CartService) {}
}
// export class CartEffects {
//   getCart$ = createEffect(() =>
//     this.actions$.pipe(
//       ofType(CartActions.GetCart),
//       mergeMap(() => {
//         return this.cartService.getCartItems().pipe(
//           map((cart: CartItem[]) =>
//             CartActions.loadCartItems({ cartItems: cart })
//           ),
//           catchError((error) =>
//             of(CartActions.GetCartFailure({ error: error }))
//           )
//         );
//       })
//     )
//   );

//   addToCart$ = createEffect(() =>
//     this.actions$.pipe(
//       ofType(CartActions.AddToCart),
//       mergeMap((action) => {
//         return this.cartService.addProductToCart(action.payload).pipe(
//           map(
//             (cart: CartItem[]) =>
//             CartActions.GetCart()
//             // CartActions.GetCartSuccess({ payload: cart })
//           ),
//           catchError((error) =>
//             of(CartActions.GetCartFailure({ error: error }))
//           )
//         );
//       })
//     )
//   );

//   removeFromCart$ = createEffect(() =>
//     this.actions$.pipe(
//       ofType(CartActions.RemoveFromCart),
//       mergeMap((action) => {
//         return this.cartService.deleteProductFromCart(action.payload).pipe(
//           map((cart: CartItem[]) => CartActions.GetCart()),
//           catchError((error) =>
//             of(CartActions.GetCartFailure({ error: error }))
//           )
//         );
//       })
//     )
//   );
