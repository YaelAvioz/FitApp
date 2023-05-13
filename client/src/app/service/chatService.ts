import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient} from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class chatService {
  baseUrl : string = "https://localhost:7248/api/Conversation"

  constructor(private store: Store, private http: HttpClient) { }

  sendMessage(username: string, msg: string) {
    const url = `${this.baseUrl}/${username}`;
    const body = { msg };
    return this.http.post<string>(url, body);
  }
}