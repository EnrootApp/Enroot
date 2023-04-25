import { AccountModel } from "../account/AccountModel";
import { StatusEnum } from "./StatusEnum";

export interface Status {
  createdOn: string;
  approver: AccountModel;
  feedbackMessage: string;
  status: StatusEnum;
}
