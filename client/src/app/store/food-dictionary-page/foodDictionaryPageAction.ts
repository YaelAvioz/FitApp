import { createAction, props } from '@ngrx/store';
import { FoodItem } from 'src/interfaces/foodItem';

export const loadFoodItems = createAction('[food-dictionary-page] load food items');
export const loadFoodItemsSuccess = createAction('[food-dictionary-page] load food items success', props<{ foodItems: FoodItem[] }>());
export const loadFoodItemsFailure = createAction('[food-dictionary-page] load food items Failure', props<{ error: any }>());

export const loadFoodItemsByLimit = createAction('[food-dictionary-page] load food items by limit',  props<{ limit: number }>());
export const loadFoodItemsByLimitSuccess = createAction('[food-dictionary-page] load food items by limit success', props<{foodItems: FoodItem[] }>());
export const loadFoodItemsByLimitFailure = createAction('[food-dictionary-page] load food items by limit Failure', props<{ error: any }>());

export const loadFoodItemsByQuery = createAction('[food-dictionary-page] load food items by query', props<{ query: string, limit: number }>());
export const loadFoodItemsByQuerySuccess = createAction('food-dictionary-page] load food items by query success', props<{ foodItems: FoodItem[] }>());
export const loadFoodItemsByQueryFailure = createAction('[food-dictionary-page] load food items by query Failure', props<{ error: any }>());

export const loadFoodItemsCountByQuery = createAction('[food-dictionary-page] load food items count by query', props<{ query: string }>());
export const loadFoodItemsCountByQuerySuccess = createAction('[food-dictionary-page] load food items count by query success', props<{ count: number }>());
export const loadFoodItemsCountByQueryFailure = createAction('[food-dictionary-page] load food items count by query Failure', props<{ error: any }>());