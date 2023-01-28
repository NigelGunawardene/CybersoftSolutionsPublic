import { StatusEnums } from "../enums/StatusEnums";
import { IUser } from "./IUser";

export interface IAuthState {
    isLoading: boolean | null;
    auth: IUser | null;
    error: string | null;
    status: StatusEnums;
  }
