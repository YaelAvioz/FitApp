import { createAction, props } from '@ngrx/store';
import { FoodItem } from 'src/interfaces/foodItem';
import { Food, FoodHistory, Grade, Login, Register, User } from 'src/interfaces/user';

export const login = createAction('[user] login', props<{ loginData: Login }>());
export const loginSuccess = createAction('[user] login success', props<{ currentUser: Login }>());
export const loginFailure = createAction('[user] login Failure', props<{ error: any }>());

export const register = createAction('[User] register User', props<{ registerData: Register }>());
export const registerSuccess = createAction('[User] register User Success', props<{ newUser: Register }>());
export const registerFailure = createAction('[User] register User Failure', props<{ error: any }>());

export const addFoodItem = createAction('[User] add food item', props<{ userId: string, foodItemId: string, amount: number }>());
export const addFoodItemSuccess = createAction('[User] add food item success');
export const addFoodItemFailure = createAction('[User] add food item failure', props<{ error: any }>());

export const loadUserByUsername = createAction('[User] load current user', props<{ username: string }>());
export const loadUserByUsernameSuccess = createAction('[User] load current user success', props<{ user: User }>());
export const loadUserByUsernameFailure = createAction('[User] load current user Failure', props<{ error: any }>());

export const loadNutritionalValues = createAction('[User] load nutritional values', props<{ username: string }>());
export const loadNutritionalValuesSuccess = createAction('[User] load nutritional values success', props<{ nutritionalValues: FoodItem }>());
export const loadNutritionalValuesFailure = createAction('[User] load nutritional values Failure', props<{ error: any }>());

export const updateUserWeight = createAction('[User] update user weight', props<{ username: string , newWeight: number }>());
export const updateUserWeightSuccess = createAction('[User] update user weight success', props<{ user: User }>());
export const updateUserWeightFailure = createAction('[User] update user weight Failure', props<{ error: any }>());

export const updateUserGoal = createAction('[User] update user goal', props<{ username: string , goal: string }>());
export const updateUserGoalSuccess = createAction('[User] update user goal success', props<{ user: User }>());
export const updateUserGoalFailure = createAction('[User] update user goal Failure', props<{ error: any }>());

export const loadUserFoodHistory = createAction('[User] load user food history', props<{ username: string }>());
export const loadUserFoodHistorySuccess = createAction('[User] load user food history success', props<{ foodHistory: FoodHistory[] }>());
export const loadUserFoodHistoryFailure = createAction('[User] load user food history Failure', props<{ error: any }>());

export const loadUserGrade = createAction('[User] load user grade', props<{ username: string }>());
export const loadUserGradeSuccess = createAction('[User] load user grade success', props<{ grade: Grade }>());
export const loadUserGradeFailure = createAction('[User] load user grade Failure', props<{ error: any }>());

export const loadUserWater = createAction('[User] load user water', props<{ username: string }>());
export const loadUserWaterSuccess = createAction('[User] load user water success', props<{ water: boolean[] }>());
export const loadUserWaterFailure = createAction('[User] load user water Failure', props<{ error: any }>());

export const updateUserWater = createAction('[User] update user water', props<{ username: string, water: boolean[] }>());
export const updateUserWaterSuccess = createAction('[User] update user water success', props<{ water: boolean[] }>());
export const updateUserWaterFailure = createAction('[User] update user water Failure', props<{ error: any }>());