import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { HomePageState } from 'src/app/store/home-page/homePageReducer';
import { loadMentors } from 'src/app/store/mentors-page/mentorsPageAction';
import { Mentor } from 'src/interfaces/mentor';

@Component({
  selector: 'app-mentors-page',
  templateUrl: './mentors-page.component.html',
  styleUrls: ['./mentors-page.component.scss']
})
export class MentorsPageComponent {
  mentors$: Observable<Mentor[]>;

  constructor(private store: Store<{homePageReducer: HomePageState}>) {
    this.mentors$ = this.store.select((state) => {    
      return state.homePageReducer.mentors;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadMentors());
  }
}