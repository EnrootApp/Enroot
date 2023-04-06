import { Assignment } from "./Assignment";

export interface Tasq {
  tasqId: string;
  creatorId: string;
  title: string;
  description: string;
  assignments: Assignment[];
}
