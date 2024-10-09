import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserState } from '../../core/models/user/userState';

export const selectUserState = createFeatureSelector<UserState>('user');

export const selectAllUsers = createSelector(
  selectUserState,
  (state: UserState) => state.users
);

export const selectUserLoading = createSelector(
  selectUserState,
  (state: UserState) => state.loading
);

export const selectUserError = createSelector(
  selectUserState,
  (state: UserState) => state.error
);
