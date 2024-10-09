import { createAction, props } from '@ngrx/store';
import { User } from '../../core/models/user/userModel';

// Action to fetch users
export const fetchUsers = createAction('[User] Fetch Users');

// Action for fetching users success
export const fetchUsersSuccess = createAction(
  '[User] Fetch Users Success',
  props<{ users: User[] }>()
);

// Action for fetching users failure
export const fetchUsersFailure = createAction(
  '[User] Fetch Users Failure',
  props<{ error: string }>()
);

// Action to create a user
export const createUser = createAction(
  '[User] Create User',
  props<{ user: User }>()
);

// Action for creating user success
export const createUserSuccess = createAction(
  '[User] Create User Success',
  props<{ user: User }>()
);

// Action for creating user failure
export const createUserFailure = createAction(
  '[User] Create User Failure',
  props<{ error: string }>()
);

// Action to update a user
export const updateUser = createAction(
  '[User] Update User',
  props<{ user: User }>()
);

// Action for updating user success
export const updateUserSuccess = createAction(
  '[User] Update User Success',
  props<{ user: User }>()
);

// Action for updating user failure
export const updateUserFailure = createAction(
  '[User] Update User Failure',
  props<{ error: string }>()
);

// Action to delete a user
export const deleteUser = createAction(
  '[User] Delete User',
  props<{ userId: string }>()
);

export const deleteUserData = createAction(
  '[User] Delete User Data',
  props<{ user: User }>()
);
// Action for deleting user success
export const deleteUserDataSuccess = createAction(
  '[User] Delete User Success',
  props<{ user: User }>()
);

// Action for deleting user success
export const deleteUserDataFailure = createAction(
  '[User] Delete User Success',
  props<{ error: string }>()
);

// Action for deleting user success
export const deleteUserSuccess = createAction(
  '[User] Delete User Success',
  props<{ userId: string }>()
);

// Action for deleting user failure
export const deleteUserFailure = createAction(
  '[User] Delete User Failure',
  props<{ error: string }>()
);
