import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { foodItemService } from '../service/foodItemService';
import { loadFoodItems, loadFoodItemsFailure, loadFoodItemsSuccess } from '../store/food-dictionary-page/foodDictionaryPageAction';


@Injectable()
export class FoodDictionaryEffects {
  constructor(
    private actions$: Actions,
    private foodItemService: foodItemService,
  ) {}
  
  loadFoodItems$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadFoodItems),
      switchMap(() =>
        this.foodItemService.getAll().pipe(
          map((foodItems) => loadFoodItemsSuccess({ foodItems })),
          catchError((error) => of(loadFoodItemsFailure({ error })))
        )
      )
    )
  );
}
