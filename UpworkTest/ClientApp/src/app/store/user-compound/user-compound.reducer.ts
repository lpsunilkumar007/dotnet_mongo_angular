import { createReducer, on } from '@ngrx/store';
import * as UserActions from './user-compound.actions';
import { UserCompoundState } from '../../core/models/user/userState';

export const initialState: UserCompoundState = {
  usersCompound: [],
  loading: false,
  error: null,
};

export const userCompoundReducer = createReducer(
  initialState,
  on(UserActions.fetchUsersCompound, (state) => {
    console.log('fetchUsers action dispatched'); // Debugging log
    return { ...state, loading: true };
  }),
  on(UserActions.fetchUsersCompoundSuccess, (state, { usersCompound }) => {
    console.log('fetchUsersSuccess action dispatched', usersCompound); // Debugging log
    
    return {
      ...state,
      usersCompound:usersCompound,
      loading: false,
      error: null,
    };
  }),
  on(UserActions.fetchUsersCompoundFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  }))
);
