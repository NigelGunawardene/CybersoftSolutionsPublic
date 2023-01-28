import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { of } from 'rxjs';
import { ICartState } from '../common/interfaces/cart.state';
import * as CartReducers from './cart.reducer'

const GetCartFromState = createFeatureSelector<ICartState>('cart');

// export const isCartLoadingSelector = createSelector(
//   GetCartFromState,
//   (state: ICartState) => state?.isLoading
// );

// export const cartSelector = createSelector(
//   GetCartFromState,
//   CartReducers.selectAllCartItems
// );

// export const cartErrorSelector = createSelector(
//   GetCartFromState,
//   (state: ICartState) => state?.error
// );

// export const cartStatusSelector = createSelector(
//   GetCartFromState,
//   (state: ICartState) => state?.status
// );



export const selectCartItemIds = createSelector(
  GetCartFromState,
  CartReducers.selectCartItemIds // shorthand for usersState => fromUser.selectUserIds(usersState)
);
export const selectCartItemEntitiesDictionary = createSelector(
  GetCartFromState,
  CartReducers.selectCartItemEntities
);
export const selectAllCartItems = createSelector(
  GetCartFromState,
  CartReducers.selectAllCartItems
);
export const selectCartItemsTotal = createSelector(
  GetCartFromState,
  CartReducers.selectCartItemsTotal
);
export const selectLoadingState = createSelector(
  GetCartFromState,
  CartReducers.getLoadingState
);
 
// export const selectCurrentUser = createSelector(
//   selectCartItemEntities,
//   selectLoadingState,
//   (userEntities, userId) => userId && userEntities[userId]
// );
