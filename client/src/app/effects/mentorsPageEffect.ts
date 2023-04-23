import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { mentorService } from '../service/mentorService';
import { loadMentors, loadMentorsFailure, loadMentorsSuccess } from '../store/mentors-page/mentorsPageAction';


@Injectable()
export class MentorsPageEffects {
  constructor(
    private actions$: Actions,
    private mentorService: mentorService,
  ) {}

  loadMentors$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadMentors),
      switchMap(() =>
        this.mentorService.getAll().pipe(
          map((mentors) => loadMentorsSuccess({ mentors })),
          catchError((error) => of(loadMentorsFailure({ error })))
        )
      )
    )
  );
}