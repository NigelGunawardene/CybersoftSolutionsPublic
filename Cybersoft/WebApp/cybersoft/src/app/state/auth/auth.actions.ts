import { createAction, props } from '@ngrx/store';
import { IUser } from '../common/interfaces/IUser';
import { AuthActionEnums } from './enums/AuthActionEnums';

export const LoginUser = createAction(
  AuthActionEnums.LOGIN,
);

export const LoginSuccess = createAction(
    AuthActionEnums.LOGIN_SUCCESS,
    props<{ payload: IUser }>() //, status: string
  );

  export const LoginFailure = createAction(
    AuthActionEnums.LOGIN_FAILURE,
    props<{ error: string }>()
  );

  export const LogoutUser = createAction(
    AuthActionEnums.LOGOUT
  );