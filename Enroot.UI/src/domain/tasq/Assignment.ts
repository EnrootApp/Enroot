import { Attachment } from "./Attachment";

export interface Assignment {
  assigneeId: string;
  assignerId: string;
  status: number;
  attachments: Attachment[];
}
