import { createReducer, on } from '@ngrx/store';
import * as UserActions from './user.actions';
import { UserState } from '../../core/models/user/userState';

export const initialState: UserState = {
  users: [],
  loading: false,
  error: null,
};

export const userReducer = createReducer(
  initialState,
  on(UserActions.fetchUsers, (state) => ({ ...state, loading: true })),
  on(UserActions.fetchUsersSuccess, (state, { users }) => ({
    ...state,
    users,
    loading: false,
    error: null,
  })),
  on(UserActions.fetchUsersFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  })),
  on(UserActions.createUser, (state, { user }) => ({
    ...state,
    users: [...state.users, user],
  })),
  on(UserActions.updateUser, (state, { user }) => ({
    ...state,
    users: state.users.map((u) => (u.id === user.id ? user : u)),
  })),
  on(UserActions.deleteUser, (state, { userId }) => ({
    ...state,
    users: state.users.filter((user) => user.id !== userId),
  }))
);
