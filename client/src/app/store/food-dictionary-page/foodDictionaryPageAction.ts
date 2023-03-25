import { createAction, props } from '@ngrx/store';
import { FoodItem } from 'src/interfaces/foodItem';

export const loadFoodItems = createAction('[food-dictionary-page] load food items');
export const loadFoodItemsSuccess = createAction('[food-dictionary-page] load food items success', props<{ foodItems: FoodItem[] }>());
export const loadFoodItemsFailure = createAction('[food-dictionary-page] load food items Failure', props<{ error: any }>());