import { createReducer, on } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';
import { loadRecipes, loadRecipesByLimit, loadRecipesByLimitFailure, loadRecipesByLimitSuccess, loadRecipesByQuery, loadRecipesByQueryFailure, loadRecipesByQuerySuccess, loadRecipesCountByQuery, loadRecipesCountByQueryFailure, loadRecipesCountByQuerySuccess, loadRecipesFailure, loadRecipesSuccess } from './recipesPageAction';

export interface recipesPageState {
    recipes: Recipe[];
    count: number,
    error: Error;
}

const initialState: recipesPageState = {
    recipes: [],
    count: 0,
    error: {} as Error,
}

export const recipesPageReducer = createReducer(
    initialState,
    on(loadRecipes, (state) => ({ ...state })),
    on(loadRecipesSuccess, (state, { recipes }) => ({ ...state, recipes: recipes, })),
    on(loadRecipesFailure, (state, { error }) => ({ ...state, recipes: error, })),
    on(loadRecipesByLimit, (state) => ({ ...state })),
    on(loadRecipesByLimitSuccess, (state, { recipes }) => ({ ...state, recipes: recipes, })),
    on(loadRecipesByLimitFailure, (state, { error }) => ({ ...state, recipes: error, })),
    on(loadRecipesByQuery, (state) => ({ ...state })),
    on(loadRecipesByQuerySuccess, (state, { recipes }) => ({ ...state, recipes: recipes, })),
    on(loadRecipesByQueryFailure, (state, { error }) => ({ ...state, recipes: error, })),
    on(loadRecipesCountByQuery, (state) => ({ ...state })),
    on(loadRecipesCountByQuerySuccess, (state, { count }) => ({ ...state, count: count, })),
    on(loadRecipesCountByQueryFailure, (state, { error }) => ({ ...state, recipes: error, })),
);
