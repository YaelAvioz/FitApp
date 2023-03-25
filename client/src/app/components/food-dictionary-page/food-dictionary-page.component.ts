import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loadFoodItems } from 'src/app/store/food-dictionary-page/foodDictionaryPageAction';
import { foodDictionaryPageState } from 'src/app/store/food-dictionary-page/foodDictionaryPageReducer';
import { FoodItem } from 'src/interfaces/foodItem';

@Component({
  selector: 'app-food-dictionary-page',
  templateUrl: './food-dictionary-page.component.html',
  styleUrls: ['./food-dictionary-page.component.scss']
})
export class FoodDictionaryPageComponent {
  foodItems$: Observable<FoodItem[]>;
  foodItems !: FoodItem[];

  constructor(private store: Store<{foodDictionaryPageReducer: foodDictionaryPageState}>) {
    this.foodItems$ = this.store.select((state) => {    
      return state.foodDictionaryPageReducer.foodItems;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadFoodItems());
    // this.foodItems$.subscribe(foodItemsToShow => {
    //   return this.foodItems = foodItemsToShow;
    // });
  }
}