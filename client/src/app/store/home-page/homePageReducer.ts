import { createReducer, on } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadMentors, loadMentorsFailure, loadMentorsSuccess } from './homePageAction';

export interface HomePageState {
    mentors: Mentor[];
    error: Error;
}

const initialState: HomePageState = {
    mentors: [],
    error: {} as Error,
}

export const homePageReducer = createReducer(
    initialState,
    on(loadMentors, (state) => ({ ...state })),
    on(loadMentorsSuccess, (state, { mentors }) => ({ ...state, mentors: mentors, })),
    on(loadMentorsFailure, (state, { error }) => ({ ...state, mentors: error, })),
);
