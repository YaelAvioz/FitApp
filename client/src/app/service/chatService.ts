import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient, HttpHeaders} from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class chatService {
  baseUrl : string = "https://localhost:7248/api/Conversation/chat"

  constructor(private store: Store, private http: HttpClient) { }

  sendMessage(username: string, msg: string) {
    const url = `${this.baseUrl}/${username}`;
    const body = JSON.stringify(msg);
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept', 'text/plain');
    return this.http.post<string>(url, body, { headers });
  }
}