import { createAction, props } from '@ngrx/store';
import { UserCompound, UserCompoundRequest } from '../../core/models/user/userModel';



// Action for fetching users success
export const fetchUsersCompound = createAction(
  '[User] Fetch Users Compound',
  props<{ usersCompound: UserCompoundRequest }>()
);
// Action for creating user success
export const fetchUsersCompoundSuccess = createAction(
  '[User] Fetch User Compound Success',
  props<{ usersCompound: UserCompound[] }>()
);

// Action for creating user failure
export const fetchUsersCompoundFailure = createAction(
  '[User] Fetch User Compound Failure',
  props<{ error: string }>()
);
