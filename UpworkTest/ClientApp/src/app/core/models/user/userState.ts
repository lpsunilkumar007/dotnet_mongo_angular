import { User } from "./userModel";

export interface UserState {
  users: User[];
  loading: boolean;
  error: string | null;
}
