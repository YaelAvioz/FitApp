import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { map, Observable, withLatestFrom } from 'rxjs';
import { loadRecipes, loadRecipesByLimit } from 'src/app/store/recipes-page/recipesPageAction';
import { recipesPageState } from 'src/app/store/recipes-page/recipesPageReducer';
import { Recipe } from 'src/interfaces/recipe';

@Component({
  selector: 'app-recipes-page',
  templateUrl: './recipes-page.component.html',
  styleUrls: ['./recipes-page.component.scss']
})
export class RecipesPageComponent {
  recipes$: Observable<Recipe[]>;
  recipes!: Recipe[];
  recipesToShow!: Recipe[];
  limit: number = 0;

  constructor(private store: Store<{ recipesPageReducer: recipesPageState }>) {
    this.recipes$ = this.store.select((state) => {
      return state.recipesPageReducer.recipes;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
    this.recipes$.subscribe(recipesToShow => {
      return this.recipes = recipesToShow
    })
  }

  next() {
    this.limit += 50;
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
    this.recipes$.subscribe(recipesToShow => {
      return this.recipes = recipesToShow
    })
  }

  prev() {
    this.limit -= 50;
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
    this.recipes$.subscribe(recipesToShow => {
      return this.recipes = recipesToShow
    })
  }
}