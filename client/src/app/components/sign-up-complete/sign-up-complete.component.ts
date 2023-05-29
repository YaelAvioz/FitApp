import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadMentorByName } from 'src/app/store/mentors-page/mentorsPageAction';
import { Observable } from 'rxjs';
import { MentorsPageState } from 'src/app/store/mentors-page/mentorPageReducer';

@Component({
  selector: 'app-sign-up-complete',
  templateUrl: './sign-up-complete.component.html',
  styleUrls: ['./sign-up-complete.component.scss']
})
export class SignUpCompleteComponent {
  @Input() mentor !: string;
  @Input() welcomeMsg !: string;
  mentor$: Observable<Mentor | null>;
  text!: string;

  constructor(private store: Store<{mentorPageReducer: MentorsPageState}>) {
    this.mentor$ = this.store.select((state) => {    
      return state.mentorPageReducer.mentor;
    })
  }

  ngOnInit(){
    this.text = this.welcomeMsg.replace(/\. /g, '.<br/>').replace(/! /g, '!<br/>');
    this.store.dispatch(loadMentorByName({name: this.mentor}));
  }
}