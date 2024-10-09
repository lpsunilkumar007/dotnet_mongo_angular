import { createAction, props } from '@ngrx/store';

export const login = createAction(
  '[Auth] Login',
  props<{ email: string; password: string }>()
);

export const logout = createAction('[Auth] Logout');

export const setAuthenticated = createAction(
  '[Auth] Set Authenticated',
  props<{ isAuthenticated: boolean }>()
);
