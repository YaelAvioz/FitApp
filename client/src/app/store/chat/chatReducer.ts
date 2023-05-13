import { createReducer, on } from '@ngrx/store';
import { sendMessage, sendMessageFailure, sendMessageSuccess } from './chatAction';

export interface ChatState {
  response: string | null;
  error: Error;
}

export const initialState: ChatState = {
  response : null,
  error: {} as Error,
};

export const chatReducer = createReducer(
  initialState,
  on(sendMessage, state => ({ ...state})),
  on(sendMessageSuccess, (state, { response }) => ({ ...state, response: response, })),
  on(sendMessageFailure, (state, { error }) => ({ ...state, response: error }))
);