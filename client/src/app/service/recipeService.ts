import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { Recipe } from 'src/interfaces/recipe';


@Injectable({
  providedIn: 'root'
})

export class recipeService {
  baseUrl : string = "https://localhost:7248/api/Recipe";

  constructor(private store: Store, private http: HttpClient) { }

  getAll() {
    return this.http.get<Recipe[]>(this.baseUrl);  
  }

  getRecipesByLimit(limit : number){
    return this.http.get<Recipe[]>(this.baseUrl + `/next/${limit}`);
  }
}