import {
  GridFilterModel,
  GridPaginationModel,
  GridRowModel,
} from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import AccountsPage from "../../../presentation/pages/Accounts/AccountsPage";
import {
  useDeleteAccountMutation,
  useGetMyAccountQuery,
  useLazyGetAccountsQuery,
  useRestoreAccountMutation,
  useSetRoleMutation,
} from "../../state/api/accountApi";
import { Permission } from "../../common/enums/permission";
import { AccountIdModel } from "./AccountsPageContainer.types";

const AccountsPageContainer = () => {
  const [account, selectAccount] = useState<AccountIdModel | null>(null);
  const [setRole] = useSetRoleMutation();
  const [deleteAccount] = useDeleteAccountMutation();
  const [restoreAccount] = useRestoreAccountMutation();

  const [getAccounts, accounts] = useLazyGetAccountsQuery();
  const { data, isFetching } = useGetMyAccountQuery({});

  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
    page: 0,
    pageSize: 25,
  });
  const [filterModel, setFilterModel] = useState<GridFilterModel>({
    items: [],
    quickFilterValues: [],
  });

  const updatePaginationModel = (model: GridPaginationModel) => {
    setPaginationModel(model);
    getAccounts({
      search: filterModel.quickFilterValues![0],
      includeDeleted:
        filterModel.items.find((i) => i.field === "includeDeleted")?.value ||
        false,
      skip: model.page * model.pageSize,
      take: model.pageSize,
    });
  };

  const onFilterModelChange = (model: GridFilterModel) => {
    setFilterModel(model);

    getAccounts({
      search: model.quickFilterValues![0],
      includeDeleted:
        model.items.find((i) => i.field === "includeDeleted")?.value || false,
      skip: paginationModel!.page * paginationModel!.pageSize,
      take: paginationModel!.pageSize,
    });
  };

  const onRowEditCommit = async (newRow: GridRowModel) => {
    await setRole({ roleId: newRow.role, accountId: newRow.id });
    return newRow;
  };

  const hasCreateAccountPermission =
    data?.permissions.includes(Permission.CreateAccount) || false;

  useEffect(() => {
    if (accounts.isUninitialized) {
      getAccounts({
        search: "",
        includeDeleted:
          filterModel.items.find((i) => i.field === "includeDeleted")?.value ||
          false,
        skip: paginationModel!.page * paginationModel!.pageSize,
        take: paginationModel!.pageSize,
      });
    }
  }, []);

  return (
    <AccountsPage
      accounts={accounts.data || { accounts: [], totalAmount: 0 }}
      isSuccess={accounts.isSuccess}
      setPaginationModel={updatePaginationModel}
      paginationModel={paginationModel}
      onFilterModelChange={onFilterModelChange}
      hasCreateAccountPermission={hasCreateAccountPermission}
      onRowEditCommit={onRowEditCommit}
      deleteAccount={account?.isDeleted ? restoreAccount : deleteAccount}
      account={account}
      selectAccount={(value: AccountIdModel | null) => selectAccount(value)}
    />
  );
};

export default AccountsPageContainer;
