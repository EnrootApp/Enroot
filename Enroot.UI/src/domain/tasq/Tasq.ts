import { AccountModel } from "../account/AccountModel";
import { Assignment } from "./Assignment";

export interface Tasq {
  id: string;
  createdOn: Date;
  creator: AccountModel;
  title: string;
  description: string;
  assignments: Assignment[];
}
