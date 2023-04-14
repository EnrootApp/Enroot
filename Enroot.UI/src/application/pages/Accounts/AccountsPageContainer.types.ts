export interface AccountsFilters {
  search: string;
  skip: number;
  take: number;
  includeDeleted: boolean;
}

export interface AccountIdModel {
  id: string;
  isDeleted: boolean;
}
