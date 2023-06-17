import { Injectable } from '@angular/core';
import { map, catchError, switchMap } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { addFoodItem, addFoodItemFailure, addFoodItemSuccess, loadNutritionalValues, loadNutritionalValuesFailure, loadNutritionalValuesSuccess, loadUserByUsername, loadUserByUsernameFailure, loadUserByUsernameSuccess, loadUserFoodHistory, loadUserFoodHistoryFailure, loadUserFoodHistorySuccess, loadUserGrade, loadUserGradeFailure, loadUserGradeSuccess, loadUserWater, loadUserWaterFailure, loadUserWaterSuccess, login, loginFailure, loginSuccess, register, registerFailure, registerSuccess, updateUserGoal, updateUserGoalFailure, updateUserGoalSuccess, updateUserWater, updateUserWaterFailure, updateUserWaterSuccess, updateUserWeight, updateUserWeightFailure, updateUserWeightSuccess } from '../store/user/userAction';
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

    addFoodItem = createEffect(() =>
        this.actions$.pipe(
            ofType(addFoodItem),
            switchMap(({ userId, foodItemId, amount }) =>
                this.userService.addFoodItem(userId, foodItemId, amount).pipe(
                    map(() => addFoodItemSuccess()),
                    catchError((error) => of(addFoodItemFailure({ error })))
                )
            )
        )
    );

    loadUserByUsername = createEffect(() =>
        this.actions$.pipe(
            ofType(loadUserByUsername),
            switchMap(({ username }) =>
                this.userService.getUserByUsername(username).pipe(
                    map((user) => loadUserByUsernameSuccess({ user })),
                    catchError((error) => of(loadUserByUsernameFailure({ error })))
                )
            )
        )
    );

    loadNutritionalValues = createEffect(() =>
        this.actions$.pipe(
            ofType(loadNutritionalValues),
            switchMap(({ username}) =>
                this.userService.getNnutritionalValues(username).pipe(
                    map((nutritionalValues) => loadNutritionalValuesSuccess({ nutritionalValues })),
                    catchError((error) => of(loadNutritionalValuesFailure({ error })))
                )
            )
        )
    );

    updateUserWeight = createEffect(() =>
        this.actions$.pipe(
            ofType(updateUserWeight),
            switchMap(({ username, newWeight }) =>
                this.userService.updateWeight(username, newWeight).pipe(
                    map((user) => updateUserWeightSuccess({ user })),
                    catchError((error) => of(updateUserWeightFailure({ error })))
                )
            )
        )
    );

    updateUserGoal = createEffect(() =>
        this.actions$.pipe(
            ofType(updateUserGoal),
            switchMap(({ username, goal }) =>
                this.userService.updateGoal(username, goal).pipe(
                    map((user) => updateUserGoalSuccess({ user })),
                    catchError((error) => of(updateUserGoalFailure({ error })))
                )
            )
        )
    );

    getUserFoodHistory = createEffect(() =>
        this.actions$.pipe(
            ofType(loadUserFoodHistory),
            switchMap(({ username }) =>
                this.userService.getUserFoodHistory(username).pipe(
                    map((foodHistory) => loadUserFoodHistorySuccess({ foodHistory })),
                    catchError((error) => of(loadUserFoodHistoryFailure({ error })))
                )
            )
        )
    );

    getUserGrade = createEffect(() =>
        this.actions$.pipe(
            ofType(loadUserGrade),
            switchMap(({ username }) =>
                this.userService.getUserGrade(username).pipe(
                    map((grade) => loadUserGradeSuccess({ grade })),
                    catchError((error) => of(loadUserGradeFailure({ error })))
                )
            )
        )
    );

    getUserWater = createEffect(() =>
        this.actions$.pipe(
            ofType(loadUserWater),
            switchMap(({ username }) =>
                this.userService.getUserWater(username).pipe(
                    map((water) => loadUserWaterSuccess({ water })),
                    catchError((error) => of(loadUserWaterFailure({ error })))
                )
            )
        )
    );

    updateUserWater = createEffect(() =>
        this.actions$.pipe(
            ofType(updateUserWater),
            switchMap(({ username, water }) =>
                this.userService.updateUserWater(username, water).pipe(
                    map((water) => updateUserWaterSuccess({ water })),
                    catchError((error) => of(updateUserWaterFailure({ error })))
                )
            )
        )
    );
}