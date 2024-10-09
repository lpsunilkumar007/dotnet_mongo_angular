// src/app/store/user/user.effects.ts
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { UserService } from '../../core/services/user/user.service';
import {
  fetchUsers,
  fetchUsersSuccess,
  fetchUsersFailure,
  createUser,
  createUserSuccess,
  createUserFailure,
  updateUser,
  updateUserSuccess,
  updateUserFailure,
  deleteUser,
  deleteUserData,
  deleteUserSuccess,
  deleteUserFailure,
  deleteUserDataSuccess,
  deleteUserDataFailure,
} from './user.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { UserDataAccessService } from '../../core/services/user-data-access/user-data-access.service';

@Injectable()
export class UserEffects {
  constructor(private actions$: Actions, private userService: UserService,private userDataAccess : UserDataAccessService) {}

  fetchUsers$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fetchUsers),
      mergeMap(() =>
        this.userService.fetchUsers().pipe(
          map(users => fetchUsersSuccess({ users })),
          catchError(error => of(fetchUsersFailure({ error: error.message })))
        )
      )
    )
  );

  createUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createUser),
      mergeMap(({ user }) =>
        this.userService.createUser(user).pipe(
          map(newUser => createUserSuccess({ user: newUser })),
          catchError(error => of(createUserFailure({ error: error.message })))
        )
      )
    )
  );

  updateUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateUser),
      mergeMap(({ user }) =>
        this.userService.updateUser(user).pipe(
          map(updatedUser => updateUserSuccess({ user: updatedUser })),
          catchError(error => of(updateUserFailure({ error: error.message })))
        )
      )
    )
  );

  deleteUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteUser),
      mergeMap(({ userId }) =>
        this.userService.deleteUser(userId).pipe(
          map(() => deleteUserSuccess({ userId })),
          catchError(error => of(deleteUserFailure({ error: error.message })))
        )
      )
    )
  );
  deleteUserData$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteUserData),
      mergeMap(({ user }) =>
        this.userDataAccess.markForDelete(user).pipe(
          map(() => deleteUserDataSuccess({ user })),
          catchError(error => of(deleteUserDataFailure({ error: error.message })))
        )
      )
    )
  );
}
