import { createReducer, on } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';
import { loadRecipes, loadRecipesByLimit, loadRecipesByLimitFailure, loadRecipesByLimitSuccess, loadRecipesByQuery, loadRecipesByQueryFailure, loadRecipesByQuerySuccess, loadRecipesFailure, loadRecipesSuccess } from './recipesPageAction';

export interface recipesPageState {
    recipes: Recipe[];
    error: Error;
}

const initialState: recipesPageState = {
    recipes: [],
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
);
