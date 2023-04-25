import { AccountModel } from "../account/AccountModel";
import { Attachment } from "./Attachment";
import { Status } from "./Status";

export interface Assignment {
  id: string;
  tasqId: string;
  createdOn: Date;
  assignee: AccountModel;
  assigner: AccountModel;
  statuses: Status[];
  attachments: Attachment[];
}
