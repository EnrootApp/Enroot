export interface TasqsFilters {
  title?: string;
  creatorId?: string;
  isAssigned?: boolean;
  isCompleted?: boolean;
  skip: number;
  take: number;
}
