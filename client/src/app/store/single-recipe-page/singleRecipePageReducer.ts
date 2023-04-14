import { createReducer, on } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';
import { loadSingleRecipe, loadSingleRecipeFailure, loadSingleRecipeSuccess } from './singleRecipePageAction';

export interface singleRecipePageState {
    recipe: Recipe;
    error: Error;
}

const initialState: singleRecipePageState = {
    recipe: {} as Recipe,
    error: {} as Error,
}

export const singleRecipePageReducer = createReducer(
    initialState,
    on(loadSingleRecipe, (state) => ({ ...state })),
    on(loadSingleRecipeSuccess, (state, { recipe }) => ({ ...state, recipe: recipe, })),
    on(loadSingleRecipeFailure, (state, { error }) => ({ ...state, recipe: error, })),
);