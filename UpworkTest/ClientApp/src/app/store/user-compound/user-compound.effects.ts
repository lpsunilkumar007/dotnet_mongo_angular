// src/app/store/user/user.effects.ts
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  fetchUsersCompound,
  fetchUsersCompoundFailure,
  fetchUsersCompoundSuccess
} from './user-compound.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { UserCompoundService } from '../../core/services/user-compound/user-compound.service';


@Injectable()
export class UserCompoundEffects {
  constructor(
    private actions$: Actions,
    private userCompoundService: UserCompoundService    
  ) {}

  fetchUsers$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fetchUsersCompound),
      mergeMap(({usersCompound}) =>
        this.userCompoundService.fetchUsersCompound(usersCompound.firstName, usersCompound.lastName).pipe(
          map((usersCompound) => {
            debugger;
            console.log('Fetched users:', usersCompound);
            return fetchUsersCompoundSuccess({ usersCompound });
          }),
          catchError((error) => {
            console.log('Error fetching users:', error.message);
            return of(fetchUsersCompoundFailure({ error: error.message }));
          })
        )
      )
    )
  );
}
