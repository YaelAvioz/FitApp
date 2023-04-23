import { createReducer, on } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadThreeMentors, loadThreeMentorsFailure, loadThreeMentorsSuccess } from './homePageAction';

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
    on(loadThreeMentors, (state) => ({ ...state })),
    on(loadThreeMentorsSuccess, (state, { mentors }) => ({ ...state, mentors: mentors, })),
    on(loadThreeMentorsFailure, (state, { error }) => ({ ...state, mentors: error, })),
);
