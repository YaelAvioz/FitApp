import { createAction, props } from '@ngrx/store';
import { Login, Register } from 'src/interfaces/user';

export const login = createAction('[user] login',  props<{ loginData: Login }>());
export const loginSuccess = createAction('[user] login success', props<{ currentUser: Login }>());
export const loginFailure = createAction('[user] login Failure', props<{ error: any }>());

export const register = createAction('[User] register User', props<{ registerData: Register }>());
export const registerSuccess = createAction('[User] register User Success',props<{ newUser: Register }>());
export const registerFailure = createAction('[User] register User Failure',props<{ error: any }>());