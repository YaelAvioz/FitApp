import { createReducer, on } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadMentors, loadMentorsFailure, loadMentorsSuccess } from './homePageAction';

export interface HomePageState {
  mentors: Mentor[];
}

export interface HomePageState {
    mentors: Mentor[];
}

const initialState: HomePageState = {
    mentors: [],
}

export const homePageReducer = createReducer(
    initialState,
    on(loadMentors, (state) => ({ ...state })),
    on(loadMentorsSuccess, (state, { mentors }) => ({ ...state, mentors: mentors, })),
    on(loadMentorsFailure, (state, { error }) => ({ ...state, mentors: error, })),
);
