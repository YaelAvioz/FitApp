import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loadFoodItemsByLimit } from 'src/app/store/food-dictionary-page/foodDictionaryPageAction';
import { foodDictionaryPageState } from 'src/app/store/food-dictionary-page/foodDictionaryPageReducer';
import { FoodItem } from 'src/interfaces/foodItem';

@Component({
  selector: 'app-food-dictionary-page',
  templateUrl: './food-dictionary-page.component.html',
  styleUrls: ['./food-dictionary-page.component.scss']
})

export class FoodDictionaryPageComponent {
  foodItems$: Observable<FoodItem[]>;
  limit: number = 0;

  constructor(private store: Store<{ foodDictionaryPageReducer: foodDictionaryPageState }>) {
    this.foodItems$ = this.store.select((state) => {
      return state.foodDictionaryPageReducer.foodItems;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
  }

  search(event: Event) {
    const value = (event.target as HTMLInputElement).value;
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
    this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
  }

  prevPage() {
    this.limit -= 20;
    this.store.dispatch(loadFoodItemsByLimit({ limit: this.limit }));
  }
}

