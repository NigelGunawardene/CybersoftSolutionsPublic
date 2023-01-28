import { createReducer, on } from '@ngrx/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as AuthActions from './auth.actions';
import { Injectable } from '@angular/core';
import { of, from } from 'rxjs';
import { switchMap, map, catchError, withLatestFrom } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IUser } from '../common/interfaces/IUser';
import { IAuthState } from '../common/interfaces/auth.state';
import { StatusEnums } from '../common/enums/StatusEnums';

const initialAuthState: IAuthState = {
  isLoading: false,
  auth: null,
  error: null,
  status: StatusEnums.PENDING,
};

export const AuthReducer = createReducer(
  initialAuthState,
  on(AuthActions.LoginUser, (state) => ({
    ...state,
    isLoading: true,
    auth: null,
    error: null,
    status: StatusEnums.LOADING,
  })),
  on(AuthActions.LoginSuccess, (state, { payload }) => ({
    ...state,
    isLoading: false,
    auth: payload,
    error: null,
    status: StatusEnums.SUCCESS,
  })),
  on(AuthActions.LoginFailure, (state, { error }) => ({
    ...state,
    isLoading: false,
    auth: null,
    error: error,
    status: StatusEnums.ERROR,
  })),
  on(AuthActions.LogoutUser, (state) => ({
    ...state,
    isLoading: false,
    auth: null,
    error: null,
    status: StatusEnums.PENDING,
  }))
);

// export const AuthReducer = createReducer(
//         // Supply the initial state
//         InitialAuthState,
//         // Add the new todo to the todos array
//         on(loginSuccess, (state, { payload, status }) => ({
//           ...state,
//           user: [...state.user, {
//             username: username,
//          }],
//         })),
//         // Remove the todo from the todos array
//         on(loginFailure, (state, { id }) => ({
//           ...state,
//           todos: state.todos.filter((todo) => todo.id !== id),
//         })),
//         // Trigger loading the todos
//         on(logoutUser, (state) => ({ ...state, status: 'loading' })),
//         // Handle successfully loaded todos
//         on(logoutUser, (state, { todos }) => ({
//           ...state,
//           todos: todos,
//           error: null,
//           status: 'success',
//         })),
//         // Handle todos load failure
//         on(logoutUser, (state, { error }) => ({
//           ...state,
//           error: error,
//           status: 'error',
//         }))
//       );

// export const AuthReducer = createReducer(
//         InitialAuthState,
//         on(loginSuccess, (state, { payload, status }) => ({
//           ...state,
//           user: [...state.user, {
//             username: username,
//          }],
//         })),
//         on(loginFailure, (state, { id }) => ({
//           ...state,
//           todos: state.todos.filter((todo) => todo.id !== id),
//         })),
//         on(logoutUser, (state) => ({ ...state, status: 'loading' })),
//         on(logoutUser, (state, { todos }) => ({
//           ...state,
//           todos: todos,
//           error: null,
//           status: 'success',
//         })),
//         on(logoutUser, (state, { error }) => ({
//           ...state,
//           error: error,
//           status: 'error',
//         }))
//       );
