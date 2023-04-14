import { createReducer, on } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadMentors, loadMentorsFailure, loadMentorsSuccess } from './mentorsPageAction';

export interface MentorsPageState {
    mentors: Mentor[];
    error: Error;
}

const initialState: MentorsPageState = {
    mentors: [],
    error: {} as Error,
}

export const mentorPageReducer = createReducer(
    initialState,
    on(loadMentors, (state) => ({ ...state })),
    on(loadMentorsSuccess, (state, { mentors }) => ({ ...state, mentors: mentors, })),
    on(loadMentorsFailure, (state, { error }) => ({ ...state, mentors: error, })),
);