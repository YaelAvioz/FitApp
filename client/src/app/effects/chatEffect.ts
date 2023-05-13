import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { chatService } from '../service/chatService';
import { sendMessage, sendMessageFailure, sendMessageSuccess } from '../store/chat/chatAction';


@Injectable()
export class chatEffects {
    constructor(
        private actions$: Actions,
        private chatService: chatService,
    ) { }

    sendMsg$ = createEffect(() =>
        this.actions$.pipe(
            ofType(sendMessage),
            switchMap(({ username, msg }) =>
                this.chatService.sendMessage(username, msg).pipe(
                    map((response) => sendMessageSuccess({ response})),
                    catchError((error) => of(sendMessageFailure({ error })))
                )
            )
        )
    );
}