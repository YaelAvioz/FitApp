import { createReducer, on } from '@ngrx/store';
import { addFoodItem, addFoodItemFailure, addFoodItemSuccess, loadUserByUsername, login, loginFailure, loginSuccess, register, registerFailure, registerSuccess, loadUserByUsernameSuccess, loadUserByUsernameFailure, loadNutritionalValues, loadNutritionalValuesSuccess, loadNutritionalValuesFailure, updateUserWeight, updateUserWeightFailure, updateUserWeightSuccess, updateUserGoal, updateUserGoalFailure, updateUserGoalSuccess, loadUserFoodHistory, loadUserFoodHistorySuccess, loadUserFoodHistoryFailure, loadUserGrade, loadUserGradeSuccess, loadUserGradeFailure } from './userAction';
import { FoodHistory, Grade, Login, Register, User } from 'src/interfaces/user';
import { FoodItem } from 'src/interfaces/foodItem';

export interface UserState {
  currentUser: Login | null;
  newUser: Register | null;
  user: User;
  nutritionalValues: FoodItem;
  foodHistory: FoodHistory[];
  grade: Grade;
  error: Error;
}

export const initialState: UserState = {
  currentUser: null,
  newUser: null,
  user: {} as User,
  nutritionalValues: {} as FoodItem,
  foodHistory: [],
  grade: {} as Grade,
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
  on(loadNutritionalValues, (state) => ({ ...state })),
  on(loadNutritionalValuesSuccess, (state, { nutritionalValues }) => ({ ...state, nutritionalValues, })),
  on(loadNutritionalValuesFailure, (state, { error }) => ({ ...state, error, })),
  on(updateUserWeight, (state) => ({ ...state })),
  on(updateUserWeightSuccess, (state, { user }) => ({ ...state, user, })),
  on(updateUserWeightFailure, (state, { error }) => ({ ...state, error, })),
  on(updateUserGoal, (state) => ({ ...state })),
  on(updateUserGoalSuccess, (state, { user }) => ({ ...state, user, })),
  on(updateUserGoalFailure, (state, { error }) => ({ ...state, error, })),
  on(loadUserFoodHistory, (state) => ({ ...state })),
  on(loadUserFoodHistorySuccess, (state, { foodHistory }) => ({ ...state, foodHistory })),
  on(loadUserFoodHistoryFailure, (state, { error }) => ({ ...state, error })),
  on(loadUserGrade, (state) => ({ ...state })),
  on(loadUserGradeSuccess, (state, { grade }) => ({ ...state, grade })),
  on(loadUserGradeFailure, (state, { error }) => ({ ...state, error })),
);