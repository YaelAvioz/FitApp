import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loadFoodItemsByLimit, loadFoodItemsByQuery, loadFoodItemsCountByQuery } from 'src/app/store/food-dictionary-page/foodDictionaryPageAction';
import { foodDictionaryPageState } from 'src/app/store/food-dictionary-page/foodDictionaryPageReducer';
import { FoodItem } from 'src/interfaces/foodItem';

@Component({
  selector: 'app-food-dictionary-page',
  templateUrl: './food-dictionary-page.component.html',
  styleUrls: ['./food-dictionary-page.component.scss']
})

export class FoodDictionaryPageComponent {
  foodItems$: Observable<FoodItem[]>;
  foodItemsCount$: Observable<number>;
  limit: number = 0;
  searchLimit: number = 0;
  value!: string;
  private debounceTimer: any;
  foodItemsCount: number = 8789;

  constructor(private store: Store<{ foodDictionaryPageReducer: foodDictionaryPageState }>) {
    this.foodItems$ = this.store.select((state) => {
      return state.foodDictionaryPageReducer.foodItems;
    })
    this. foodItemsCount$ = this.store.select((state) => {
      return state.foodDictionaryPageReducer.count;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
  }

  search(event: Event) {
    this.searchLimit = 0;
    clearTimeout(this.debounceTimer);
    this.debounceTimer = setTimeout(() => {
      this.value = (event.target as HTMLInputElement).value;
      if (this.value == '') {
        this.limit = 0;
        this.foodItemsCount = 8789;
        this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
      }
      else {
        this.store.dispatch(loadFoodItemsByQuery({ query: this.value, limit: this.searchLimit }));
        this.store.dispatch(loadFoodItemsCountByQuery({ query: this.value }));
        this.foodItemsCount$.subscribe(value => {
          this.foodItemsCount = value;
        });
      }
    }, 2000);
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
      this.store.dispatch(loadFoodItemsByQuery({ query: this.value, limit: this.searchLimit }));
    }
    else {
      this.foodItemsCount = 8789;
      this.limit += 12;
      this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
    }
  }

  prevPage() {
    if (this.value) {
      this.searchLimit -= 12;
      this.store.dispatch(loadFoodItemsByQuery({ query: this.value, limit: this.searchLimit }));
    }
    else {
      this.foodItemsCount = 8789;
      this.limit -= 12;
      this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
    }
  }
}