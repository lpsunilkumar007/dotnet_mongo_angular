import { User, UserCompound } from "./userModel";

export interface UserState {
  users: User[];
  loading: boolean;
  error: string | null;
}

export interface UserCompoundState {
  usersCompound: UserCompound[];
  loading: boolean;
  error: string | null;
}
