import { createReducer, on } from '@ngrx/store';
import { Mentor } from 'src/interfaces/mentor';
import { loadMentorByName, loadMentorByNameFailure, loadMentorByNameSuccess, loadMentors, loadMentorsFailure, loadMentorsSuccess } from './mentorsPageAction';

export interface MentorsPageState {
    mentors: Mentor[];
    mentor: Mentor | null;
    error: Error;
}

const initialState: MentorsPageState = {
    mentors: [],
    mentor: null,
    error: {} as Error,
}

export const mentorPageReducer = createReducer(
    initialState,
    on(loadMentors, (state) => ({ ...state })),
    on(loadMentorsSuccess, (state, { mentors }) => ({ ...state, mentors: mentors, })),
    on(loadMentorsFailure, (state, { error }) => ({ ...state, mentors: error, })),

    on(loadMentorByName, (state) => ({ ...state })),
    on(loadMentorByNameSuccess, (state, { mentor }) => ({ ...state, mentor: mentor, })),
    on(loadMentorByNameFailure, (state, { error }) => ({ ...state, mentor: error, })),
);