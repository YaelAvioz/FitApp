import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { FoodItem } from 'src/interfaces/foodItem';


@Injectable({
  providedIn: 'root'
})

export class foodItemService {
  baseUrl : string = "https://localhost:7248/api/food";

  constructor(private store: Store, private http: HttpClient) { }

  getAll() {
    return this.http.get<FoodItem[]>(this.baseUrl);
  }

  getFoodItemsByLimit(limit : number){
    return this.http.get<FoodItem[]>(this.baseUrl + `/next/${limit}`);
  }
}