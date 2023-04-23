import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { Mentor } from 'src/interfaces/mentor';


@Injectable({
  providedIn: 'root'
})

export class mentorService {
  baseUrl: string = "https://localhost:7248/api/mentor";

  constructor(private store: Store, private http: HttpClient) { }

  getAll() {
    return this.http.get<Mentor[]>(this.baseUrl);
  }

  getThreeMentors() {
    return this.http.get<Mentor[]>(this.baseUrl + `/getThreeMentors`);
  }
}