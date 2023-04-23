import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { HomePageState } from 'src/app/store/home-page/homePageReducer';
import { MentorsPageState } from 'src/app/store/mentors-page/mentorPageReducer';
import { loadMentors } from 'src/app/store/mentors-page/mentorsPageAction';
import { Mentor } from 'src/interfaces/mentor';

@Component({
  selector: 'app-mentors-page',
  templateUrl: './mentors-page.component.html',
  styleUrls: ['./mentors-page.component.scss']
})
export class MentorsPageComponent {
  mentors$: Observable<Mentor[]>;

  constructor(private store: Store<{mentorPageReducer: MentorsPageState}>) {
    this.mentors$ = this.store.select((state) => {    
      return state.mentorPageReducer.mentors;
    })
  }

  ngOnInit() {
    this.store.dispatch(loadMentors());
  }
}