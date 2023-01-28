import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IAuthState } from '../common/interfaces/auth.state';
import { of } from 'rxjs';

const GetAuthFromState = createFeatureSelector<IAuthState>('auth');
// export const GetAuthFromState = (state: IAppState) => state.auth;


export const isAuthLoadingSelector = createSelector(
  GetAuthFromState,
  (state: IAuthState) => state?.isLoading
);

export const authSelector = createSelector(
  GetAuthFromState,
  (state: IAuthState) => state?.auth
);

export const authErrorSelector = createSelector(
  GetAuthFromState,
  (state: IAuthState) => state?.error
);

export const authStatusSelector = createSelector(
  GetAuthFromState,
  (state: IAuthState) => state?.status
);





// export const allState = (state: IAppState) => state;
// export const loggedInUser = createFeatureSelector<IAuthState>('auth');

// export const loggedInUser = createFeatureSelector<AuthState>('user');

// export const LoggedInUserSelector = createSelector(
//   loggedInUser,
//   (state: IAuthState) => state.user
// );

// export const UsernameSelector = createSelector(
//   loggedInUser,
//   (state: IAuthState) => state.user
// );

// export const SelectLoggedInUser = createSelector(
//   loggedInUser,
//   (state: AuthState) => state.user
// );

// export const selectFeature = (state: IAppState) => state.user;

// export const selectFeatureCount = createSelector(
//   selectFeature,
//   (state: AuthState) => state.user
// );
