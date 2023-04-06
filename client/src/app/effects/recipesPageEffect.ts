import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { loadRecipes, loadRecipesByLimit, loadRecipesByLimitFailure, loadRecipesByLimitSuccess, loadRecipesFailure, loadRecipesSuccess } from '../store/recipes-page/recipesPageAction';
import { recipeService } from '../service/recipeService';


@Injectable()
export class RecipesEffects {
  constructor(
    private actions$: Actions,
    private recipeService: recipeService
  ) {}
  
  loadRecipes$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadRecipes),
      switchMap(() =>
        this.recipeService.getAll().pipe(
          map((recipes) => loadRecipesSuccess({ recipes })),
          catchError((error) => of(loadRecipesFailure({ error })))
        )
      )
    )
  );

  loadRecipesByLimit$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadRecipesByLimit),
      switchMap(({limit}) =>
        this.recipeService.getRecipesByLimit(limit).pipe(
          map((recipes) => loadRecipesByLimitSuccess({ recipes })),
          catchError((error) => of(loadRecipesByLimitFailure({ error })))
        )
      )
    )
  );
}
