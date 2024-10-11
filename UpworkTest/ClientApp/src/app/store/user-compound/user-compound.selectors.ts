import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserCompoundState } from '../../core/models/user/userState';

export const selectUserState = createFeatureSelector<UserCompoundState>('usersCompound');

export const selectAllUsersCompound = createSelector(
  selectUserState,
  (state: UserCompoundState) => state.usersCompound  
);

export const selectUserCompoundLoading = createSelector(
  selectUserState,
  (state: UserCompoundState) => state.loading
);

export const selectUserCompoundError = createSelector(
  selectUserState,
  (state: UserCompoundState) => state.error
);
