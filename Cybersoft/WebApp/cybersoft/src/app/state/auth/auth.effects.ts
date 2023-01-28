import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { AccountService } from 'src/app/services/account';
import { IUser } from '../common/interfaces/IUser';
import * as AuthActions from './auth.actions';

@Injectable()
export class AuthEffects {
  getLoggedInUserDetails$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.LoginUser),
      mergeMap(() => {
        return this.accountService.getLoggedInUserState().pipe(
          map((user: IUser) => AuthActions.LoginSuccess({ payload: user })),
          catchError((error) => of(AuthActions.LoginFailure({ error: error })))
        );
      })
    )
  );

  constructor(
    private actions$: Actions,
    private accountService: AccountService
  ) {}
}

// export class AuthEffects {
//     getLoggedInUserDetails$ = createEffect(() =>
//       this.actions$.pipe(
//         ofType(AuthActions.LoginUser),
//         mergeMap(() => {
//           return this.accountService
//             .getByUserName(this.accountService.getLoggedInUserDetails('name'))
//             .pipe(map((user) => AuthActions.LoginSuccess({ user })));
//         })
//       )
//     );
