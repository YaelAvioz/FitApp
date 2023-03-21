import { createAction, props } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';

export const loadMentors = createAction('[home-page] load mentors');
export const loadMentorsSuccess = createAction('[home-page] load mentors success',props<{ mentors: Mentor[] }>());
export const loadMentorsFailure = createAction('[home-page] load mentors Failure',props<{ error: any }>());