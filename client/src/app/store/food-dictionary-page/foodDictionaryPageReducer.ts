import { createReducer, on } from '@ngrx/store';
import { FoodItem } from 'src/interfaces/foodItem';
import { loadFoodItems, loadFoodItemsByLimit, loadFoodItemsByLimitFailure, loadFoodItemsByLimitSuccess, loadFoodItemsByQuery, loadFoodItemsByQueryFailure, loadFoodItemsByQuerySuccess, loadFoodItemsCountByQuery, loadFoodItemsCountByQueryFailure, loadFoodItemsCountByQuerySuccess, loadFoodItemsFailure, loadFoodItemsSuccess } from './foodDictionaryPageAction';

export interface foodDictionaryPageState {
    foodItems: FoodItem[];
    count: number,
    error: Error;
}

const initialState: foodDictionaryPageState = {
    foodItems: [],
    count: 0,
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

    on(loadFoodItemsByQuery, (state) => ({ ...state })),
    on(loadFoodItemsByQuerySuccess, (state, { foodItems }) => ({ ...state, foodItems: foodItems, })),
    on(loadFoodItemsByQueryFailure, (state, { error }) => ({ ...state, foodItems: error, })),
    on(loadFoodItemsCountByQuery, (state) => ({ ...state })),
    on(loadFoodItemsCountByQuerySuccess, (state, { count }) => ({ ...state, count: count, })),
    on(loadFoodItemsCountByQueryFailure, (state, { error }) => ({ ...state, foodItems: error, })),
);
