import { createAction, props } from '@ngrx/store';

export const sendMessage = createAction('[chat] send message', props<{ username: string; msg: string }>());
export const sendMessageSuccess = createAction('[chat] send message success', props<{ response: string }>());
export const sendMessageFailure = createAction('[chat] send message failure', props<{ error: any }>());