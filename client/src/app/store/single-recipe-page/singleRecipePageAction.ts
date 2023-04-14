import { createAction, props } from '@ngrx/store';
import { Recipe } from 'src/interfaces/recipe';

export const loadSingleRecipe = createAction('[single-recipe-page] load recipe',  props<{ recipeName: string }>());
export const loadSingleRecipeSuccess = createAction('[single-recipe-page] load recipe success', props<{ recipe: Recipe }>());
export const loadSingleRecipeFailure = createAction('[single-recipe-page] load recipe Failure', props<{ error: any }>());