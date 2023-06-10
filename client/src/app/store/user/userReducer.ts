import { createReducer, on } from '@ngrx/store';
import { addFoodItem, addFoodItemFailure, addFoodItemSuccess, loadUserByUsername, login, loginFailure, loginSuccess, register, registerFailure, registerSuccess, loadUserByUsernameSuccess, loadUserByUsernameFailure } from './userAction';
import { Login, Register, User } from 'src/interfaces/user';

export interface UserState {
  currentUser: Login | null;
  newUser: Register | null;
  user: User;
  error: Error;
}

export const initialState: UserState = {
  currentUser: null,
  newUser: null,
  user: {} as User,
  error: {} as Error,
};

export const userReducer = createReducer(
  initialState,
  on(login, (state) => ({ ...state })),
  on(loginSuccess, (state, { currentUser }) => ({ ...state, currentUser, })),
  on(loginFailure, (state, { error }) => ({ ...state, error, })),
  on(register, (state) => ({ ...state })),
  on(registerSuccess, (state, { newUser }) => ({ ...state, newUser, })),
  on(registerFailure, (state, { error }) => ({ ...state, error, })),
  on(addFoodItem, (state) => ({ ...state })),
  on(addFoodItemSuccess, (state) => ({ ...state })),
  on(addFoodItemFailure, (state, { error }) => ({ ...state, error, })),
  on(loadUserByUsername, (state) => ({ ...state })),
  on(loadUserByUsernameSuccess, (state, { user }) => ({ ...state, user, })),
  on(loadUserByUsernameFailure, (state, { error }) => ({ ...state, error, })),
);