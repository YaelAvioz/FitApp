import { createAction, props } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';

export const loadThreeMentors = createAction('[home-page] load mentors');
export const loadThreeMentorsSuccess = createAction('[home-page] load mentors success',props<{ mentors: Mentor[] }>());
export const loadThreeMentorsFailure = createAction('[home-page] load mentors Failure',props<{ error: any }>());