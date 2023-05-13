import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { login, loginFailure, loginSuccess, register, registerFailure, registerSuccess } from '../store/user/userAction';
import { userService } from '../service/userService';

@Injectable()
export class userEffects {
    constructor(
        private actions$: Actions,
        private userService: userService,
    ) { }

    login$ = createEffect(() =>
        this.actions$.pipe(
            ofType(login),
            switchMap(({ loginData }) =>
                this.userService.login(loginData).pipe(
                    map((currentUser) => loginSuccess({ currentUser })),
                    catchError((error) => of(loginFailure({ error })))
                )
            )
        )
    );

    register$ = createEffect(() =>
        this.actions$.pipe(
            ofType(register),
            switchMap(({ registerData }) =>
                this.userService.register(registerData).pipe(
                    map((newUser) => registerSuccess({ newUser })),
                    catchError((error) => of(registerFailure({ error })))
                )
            )
        )
    );
}