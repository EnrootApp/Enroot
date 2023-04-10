import { AccountModel } from "../account/AccountModel";
import { Attachment } from "./Attachment";

export interface Assignment {
  id: string;
  tasqId: string;
  createdOn: Date;
  assignee: AccountModel;
  assigner: AccountModel;
  status: number;
  attachments: Attachment[];
}
