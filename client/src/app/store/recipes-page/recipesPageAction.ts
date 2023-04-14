import { createAction, props } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';

export const loadRecipes = createAction('[recipes-page] load recipes');
export const loadRecipesSuccess = createAction('[recipes-page] load recipes success', props<{ recipes: Recipe[] }>());
export const loadRecipesFailure = createAction('[recipes-page] load recipes Failure', props<{ error: any }>());

export const loadRecipesByLimit = createAction('[recipes-page] load recipes by limit',  props<{ limit: number }>());
export const loadRecipesByLimitSuccess = createAction('[recipes-page] load recipes by limit success', props<{ recipes: Recipe[] }>());
export const loadRecipesByLimitFailure = createAction('[recipes-page] load recipes by limit Failure', props<{ error: any }>());