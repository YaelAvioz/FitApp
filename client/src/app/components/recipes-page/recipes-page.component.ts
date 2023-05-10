import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Store } from '@ngrx/store';
import { debounceTime, fromEvent, map, Observable, Subject, withLatestFrom } from 'rxjs';
import { loadRecipes, loadRecipesByLimit, loadRecipesByQuery } from 'src/app/store/recipes-page/recipesPageAction';
import { recipesPageState } from 'src/app/store/recipes-page/recipesPageReducer';
import { Recipe } from 'src/interfaces/recipe';

@Component({
  selector: 'app-recipes-page',
  templateUrl: './recipes-page.component.html',
  styleUrls: ['./recipes-page.component.scss']
})
export class RecipesPageComponent {
  recipes$: Observable<Recipe[]>;
  limit: number = 0;
  private debounceTimer: any;

  constructor(private store: Store<{ recipesPageReducer: recipesPageState }>) {
    this.recipes$ = this.store.select((state) => {
      return state.recipesPageReducer.recipes;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
  }

  search(event: Event) {
    clearTimeout(this.debounceTimer);
    this.debounceTimer = setTimeout(() => {
      const value = (event.target as HTMLInputElement).value;
      this.store.dispatch(loadRecipesByQuery({ query: value }));
    }, 3000);
  }

  onPageChange(event: PageEvent) {
    if (event.pageIndex > (event.previousPageIndex ?? -1)) {
      this.nextPage();
    } else {
      this.prevPage();
    }
  }

  nextPage() {
    this.limit += 20;
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
  }

  prevPage() {
    this.limit -= 20;
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
  }
}