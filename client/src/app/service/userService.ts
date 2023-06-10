import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login, Register, User } from 'src/interfaces/user';
import { FoodItem } from 'src/interfaces/foodItem';


@Injectable({
  providedIn: 'root'
})

export class userService {
  baseUrl: string = "https://localhost:7248/api/Account";

  constructor(private store: Store, private http: HttpClient) { }

  login(LoginInfo: Login): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, LoginInfo);
  }

  register(registerInfo: Register): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, registerInfo);
  }

  addFoodItem(userId: string, foodItemId: string, amount:number){
    const payload = {
      foodId: foodItemId,
      amount: amount
    };
    return this.http.post<any>(this.baseUrl + `/${userId}/food`, payload);
  }

  getUserByUsername(username: string){
    return this.http.get<User>(this.baseUrl + `/username/${username}`);
  }

  getNnutritionalValues(userId: string){
    return this.http.get<FoodItem>(this.baseUrl + `/${userId}/recent-food`);
  }
}