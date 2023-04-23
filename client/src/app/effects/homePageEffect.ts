import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { loadThreeMentors, loadThreeMentorsFailure, loadThreeMentorsSuccess } from '../store/home-page/homePageAction';
import { mentorService } from '../service/mentorService';


@Injectable()
export class HomePageEffects {
  constructor(
    private actions$: Actions,
    private mentorService: mentorService,
  ) {}

  loadThreeMentors$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadThreeMentors),
      switchMap(() =>
        this.mentorService.getThreeMentors().pipe(
          map((mentors) => loadThreeMentorsSuccess({ mentors })),
          catchError((error) => of(loadThreeMentorsFailure({ error })))
        )
      )
    )
  );
}