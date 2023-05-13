import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login, Register } from 'src/interfaces/user';


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
}