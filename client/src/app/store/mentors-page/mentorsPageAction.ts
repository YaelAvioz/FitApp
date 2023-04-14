import { createAction, props } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';

export const loadMentors = createAction('[mentors-page] load mentors');
export const loadMentorsSuccess = createAction('[mentors-page] load mentors success',props<{ mentors: Mentor[] }>());
export const loadMentorsFailure = createAction('[mentors-page] load mentors Failure',props<{ error: any }>());