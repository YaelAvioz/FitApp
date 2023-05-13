import { createReducer, on } from '@ngrx/store';
import { login, loginFailure, loginSuccess, register, registerFailure, registerSuccess } from './userAction';
import { Login } from 'src/interfaces/user';

export interface UserState {
  currentUser: Login | null;
  error: Error;
}

export const initialState: UserState = {
  currentUser: null,
  error: {} as Error,
};

export const userReducer = createReducer(
  initialState,
  on(login, (state) => ({ ...state })),
  on(loginSuccess, (state, { currentUser }) => ({ ...state, currentUser, })),
  on(loginFailure, (state, { error }) => ({ ...state, error, })),
  on(register, (state) => ({ ...state })),
  on(registerSuccess, (state, { newUser }) => ({ ...state, newUser, })),
  on(registerFailure, (state, { error }) => ({ ...state, error, }))
);