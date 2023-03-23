import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loadMentors } from 'src/app/store/home-page/homePageAction';
import { HomePageState } from 'src/app/store/home-page/homePageReducer';
import { Mentor } from 'src/interfaces/mentor';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
  mentors$: Observable<Mentor[]>;
  mentors:Mentor[] | undefined;

  constructor(private store: Store<any>) {
    this.mentors$ = this.store.select((state) => state.homePageReducer.mentor)
  }

  ngOnInit() {
    this.store.dispatch(loadMentors());
    this.mentors$.subscribe((data) => {
      this.mentors = data;
    });
    console.log(this.mentors);
  }
}
