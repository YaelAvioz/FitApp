import { createAction, props } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';

export const loadRecipes = createAction('[recipes-page] load recipes');
export const loadRecipesSuccess = createAction('[recipes-page] load recipes success', props<{ recipes: Recipe[] }>());
export const loadRecipesFailure = createAction('[recipes-page] load recipes Failure', props<{ error: any }>());

export const loadRecipesByLimit = createAction('[recipes-page] load recipes by limit', props<{ limit: number }>());
export const loadRecipesByLimitSuccess = createAction('[recipes-page] load recipes by limit success', props<{ recipes: Recipe[] }>());
export const loadRecipesByLimitFailure = createAction('[recipes-page] load recipes by limit Failure', props<{ error: any }>());

export const loadRecipesByQuery = createAction('[recipes-page] load recipes by query', props<{ query: string, limit: number }>());
export const loadRecipesByQuerySuccess = createAction('[recipes-page] load recipes by query success', props<{ recipes: Recipe[] }>());
export const loadRecipesByQueryFailure = createAction('[recipes-page] load recipes by query Failure', props<{ error: any }>());

export const loadRecipesCountByQuery = createAction('[recipes-page] load recipes count by query', props<{ query: string }>());
export const loadRecipesCountByQuerySuccess = createAction('[recipes-page] load recipes count by query success', props<{ count: number }>());
export const loadRecipesCountByQueryFailure = createAction('[recipes-page] load recipes count by query Failure', props<{ error: any }>());