import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Store } from '@ngrx/store';
import { debounceTime, fromEvent, map, Observable, Subject, withLatestFrom } from 'rxjs';
import { loadRecipes, loadRecipesByLimit, loadRecipesByQuery, loadRecipesCountByQuery } from 'src/app/store/recipes-page/recipesPageAction';
import { recipesPageState } from 'src/app/store/recipes-page/recipesPageReducer';
import { Recipe } from 'src/interfaces/recipe';

@Component({
  selector: 'app-recipes-page',
  templateUrl: './recipes-page.component.html',
  styleUrls: ['./recipes-page.component.scss']
})
export class RecipesPageComponent {
  recipes$: Observable<Recipe[]>;
  recipesCount$: Observable<number>;
  limit: number = 0;
  searchLimit: number = 0;
  value!: string;
  private debounceTimer: any;
  recipesCount: number = 12239;

  constructor(private store: Store<{ recipesPageReducer: recipesPageState }>) {
    this.recipes$ = this.store.select((state) => {
      return state.recipesPageReducer.recipes;
    })
    this.recipesCount$ = this.store.select((state) => {
      return state.recipesPageReducer.count;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
  }

  search(event: Event) {
    this.searchLimit = 0;
    clearTimeout(this.debounceTimer);
    this.debounceTimer = setTimeout(() => {
      this.value = (event.target as HTMLInputElement).value;
      if (this.value == '') {
        this.limit = 0;
        this.recipesCount = 12239;
        this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
      }
      else {
        this.store.dispatch(loadRecipesByQuery({ query: this.value, limit: this.searchLimit }));
        this.store.dispatch(loadRecipesCountByQuery({ query: this.value }));
        this.recipesCount$.subscribe(value => {
          this.recipesCount = value;
        });
      }
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
    if (this.value) {
      this.searchLimit += 12;
      this.store.dispatch(loadRecipesByQuery({ query: this.value, limit: this.searchLimit }));
    }
    else {
      this.recipesCount = 12239;
      this.limit += 12;
      this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
    }
  }

  prevPage() {
    if (this.value) {
      this.searchLimit -= 12;
      this.store.dispatch(loadRecipesByQuery({ query: this.value, limit: this.searchLimit }));
    }
    else {
      this.recipesCount = 12239;
      this.limit -= 12;
      this.store.dispatch(loadRecipesByLimit({ limit: this.limit }));
    }
  }
}