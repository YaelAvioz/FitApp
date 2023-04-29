import { createReducer, on } from '@ngrx/store';
import { FoodItem } from 'src/interfaces/foodItem';
import { loadFoodItems, loadFoodItemsByLimit, loadFoodItemsByLimitFailure, loadFoodItemsByLimitSuccess, loadFoodItemsFailure, loadFoodItemsSuccess } from './foodDictionaryPageAction';

export interface foodDictionaryPageState {
    foodItems: FoodItem[];
    error: Error;
}

const initialState: foodDictionaryPageState = {
    foodItems: [],
    error: {} as Error,
}

export const foodDictionaryPageReducer = createReducer(
    initialState,
    on(loadFoodItems, (state) => ({ ...state })),
    on(loadFoodItemsSuccess, (state, { foodItems }) => ({ ...state, foodItems: foodItems, })),
    on(loadFoodItemsFailure, (state, { error }) => ({ ...state, foodItems: error, })),
    on(loadFoodItemsByLimit, (state) => ({ ...state })),
    on(loadFoodItemsByLimitSuccess, (state, { foodItems }) => ({ ...state, foodItems: foodItems, })),
    on(loadFoodItemsByLimitFailure, (state, { error }) => ({ ...state, foodItems: error, })),
);
