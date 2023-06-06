import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { loadRecipes, loadRecipesByLimit, loadRecipesByLimitFailure, loadRecipesByLimitSuccess, loadRecipesByQuery, loadRecipesByQueryFailure, loadRecipesByQuerySuccess, loadRecipesCountByQuery, loadRecipesCountByQueryFailure, loadRecipesCountByQuerySuccess, loadRecipesFailure, loadRecipesSuccess } from '../store/recipes-page/recipesPageAction';
import { recipeService } from '../service/recipeService';
import { loadSingleRecipe, loadSingleRecipeFailure, loadSingleRecipeSuccess } from '../store/single-recipe-page/singleRecipePageAction';


@Injectable()
export class RecipesEffects {
  constructor(
    private actions$: Actions,
    private recipeService: recipeService
  ) { }

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
      switchMap(({ limit }) =>
        this.recipeService.getRecipesByLimit(limit).pipe(
          map((recipes) => loadRecipesByLimitSuccess({ recipes })),
          catchError((error) => of(loadRecipesByLimitFailure({ error })))
        )
      )
    )
  );

  loadSingleRecipe$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadSingleRecipe),
      switchMap(({ recipeName }) =>
        this.recipeService.getSingleRecipe(recipeName).pipe(
          map((recipe) => loadSingleRecipeSuccess({ recipe })),
          catchError((error) => of(loadSingleRecipeFailure({ error })))
        )
      )
    )
  );

  loadRecipesByQuery$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadRecipesByQuery),
      switchMap(({ query, limit }) =>
        this.recipeService.getRecipesByQuery(query, limit).pipe(
          map((recipes) => loadRecipesByQuerySuccess({ recipes })),
          catchError((error) => of(loadRecipesByQueryFailure({ error })))
        )
      )
    )
  );

  loadRecipesCountByQuery$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadRecipesCountByQuery),
      switchMap(({ query }) =>
        this.recipeService.getRecipesCountByQuery(query).pipe(
          map((count) => loadRecipesCountByQuerySuccess({ count })),
          catchError((error) => of(loadRecipesCountByQueryFailure({ error })))
        )
      )
    )
  );

}
