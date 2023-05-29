import { createAction, props } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';

export const loadMentors = createAction('[mentors-page] load mentors');
export const loadMentorsSuccess = createAction('[mentors-page] load mentors success',props<{ mentors: Mentor[] }>());
export const loadMentorsFailure = createAction('[mentors-page] load mentors Failure',props<{ error: any }>());

export const loadMentorByName = createAction('[sign-up-page] load mentor by name',  props<{ name: string }>());
export const loadMentorByNameSuccess = createAction('[sign-up-page] load mentor by name success', props<{ mentor: Mentor }>());
export const loadMentorByNameFailure = createAction('[sign-up-page] load mentor by name Failure', props<{ error: any }>());