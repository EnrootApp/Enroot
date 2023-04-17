import React from "react";
import {
  DataGrid,
  GridCallbackDetails,
  GridColDef,
  GridFilterModel,
  GridPaginationModel,
  GridRowModel,
  enUS,
  ruRU,
} from "@mui/x-data-grid";
import { Avatar, Box, IconButton } from "@mui/material";
import AccountsToolbar from "../../components/AccountsToolbar/AccountsToolbar";
import strings from "../../localization/locales";
import { AccountModel } from "../../../domain/account/AccountModel";
import { Role } from "../../../application/common/enums/role";
import { Cancel, Check, Delete, RestoreFromTrash } from "@mui/icons-material";
import ConfirmationDialog from "../../components/ConfirmationDialog/ConfirmationDialog";
import { AccountIdModel } from "../../../application/pages/Accounts/AccountsPageContainer.types";

interface Props {
  accounts: { accounts: AccountModel[]; totalAmount: number };
  isSuccess: boolean;
  onFilterModelChange: (
    model: GridFilterModel,
    details: GridCallbackDetails<"filter">
  ) => void;
  setPaginationModel: (model: GridPaginationModel) => void;
  paginationModel: GridPaginationModel;
  hasCreateAccountPermission: boolean;
  onRowEditCommit: (newRow: GridRowModel, oldRow: GridRowModel) => void;
  deleteAccount: (value: AccountIdModel) => void;
  account: AccountIdModel | null;
  selectAccount: (value: AccountIdModel | null) => void;
}

const AccountsPage: React.FC<Props> = ({
  accounts,
  isSuccess,
  onFilterModelChange,
  setPaginationModel,
  paginationModel,
  hasCreateAccountPermission,
  onRowEditCommit,
  deleteAccount,
  account,
  selectAccount,
}) => {
  const roleNameMap = {
    [Role.Default]: strings.defaultRole,
    [Role.Moderator]: strings.moderatorRole,
    [Role.TenantAdmin]: strings.tenantAdminRole,
  };

  const rows =
    accounts?.accounts.map((account) => ({
      model: { imageSrc: account.avatarUrl, name: account.name },
      ...account,
    })) || [];

  const columns: GridColDef[] = [
    {
      field: "avatarUrl",
      headerName: strings.avatar,
      flex: 0,
      width: 80,
      renderCell: (params) => <Avatar src={params.value} />,
      sortable: false,
      filterable: false,
    },
    {
      field: "name",
      headerName: strings.employee,
      flex: 1,
      minWidth: 250,
      sortable: false,
      filterable: false,
    },
    {
      field: "email",
      headerName: strings.email,
      flex: 1,
      minWidth: 250,
      sortable: false,
      filterable: false,
    },
    {
      field: "createdOn",
      headerName: strings.added,
      renderCell: (params) => new Date(params.value).toLocaleString(),
      flex: 1,
      minWidth: 250,
      sortable: false,
      filterable: false,
    },
    {
      field: "role",
      headerName: strings.role,
      renderCell: (params: { value?: Role }) =>
        roleNameMap[params.value || Role.Default],
      flex: 1,
      minWidth: 250,
      sortable: false,
      filterable: false,
      editable: hasCreateAccountPermission,
      type: "singleSelect",
      valueOptions: [
        { value: Role.Default, label: roleNameMap[Role.Default] },
        { value: Role.Moderator, label: roleNameMap[Role.Moderator] },
        { value: Role.TenantAdmin, label: roleNameMap[Role.TenantAdmin] },
      ],
    },
  ];

  if (hasCreateAccountPermission) {
    columns.push({
      field: "includeDeleted",
      headerName: strings.deleted,
      renderCell: (params) => (params.value ? <Check /> : <Cancel />),
      width: 100,
      sortable: false,
      type: "boolean",
      valueGetter: (row) => row.row.isDeleted,
    });
    columns.push({
      field: "actions",
      headerName: strings.actions,
      renderCell: (params) => (
        <IconButton
          onClick={() =>
            selectAccount({
              id: params.row.id,
              isDeleted: params.row.isDeleted,
            })
          }
        >
          {params.row.isDeleted ? <RestoreFromTrash /> : <Delete />}
        </IconButton>
      ),
      width: 100,
      sortable: false,
      filterable: false,
    });
  }

  return (
    <Box sx={{ width: "100%" }}>
      <DataGrid
        isCellEditable={(params) => !params.row.isDeleted}
        getRowId={(row) => row.id}
        rows={rows}
        columns={columns}
        autoHeight
        loading={!isSuccess}
        paginationMode="server"
        filterMode="server"
        onFilterModelChange={onFilterModelChange}
        initialState={{
          pagination: {
            paginationModel: { pageSize: 25, page: 0 },
          },
        }}
        pageSizeOptions={[5, 10, 25]}
        onPaginationModelChange={setPaginationModel}
        paginationModel={paginationModel}
        rowCount={accounts?.totalAmount || 0}
        localeText={
          localStorage.getItem("lang") === "ruRU"
            ? ruRU.components.MuiDataGrid.defaultProps.localeText
            : enUS.components.MuiDataGrid.defaultProps.localeText
        }
        slots={{ toolbar: AccountsToolbar }}
        disableRowSelectionOnClick
        processRowUpdate={(newRow, oldRow) => onRowEditCommit(newRow, oldRow)}
        onProcessRowUpdateError={(error) => console.log(error)}
      />
      <ConfirmationDialog
        open={account !== null}
        title={
          account?.isDeleted
            ? strings.restoreAccountConfirmation
            : strings.deleteAccountConfirmation
        }
        onAgree={() => {
          deleteAccount(account!);
        }}
        onDisagree={() => {}}
        onClose={() => {
          selectAccount(null);
        }}
      />
    </Box>
  );
};

export default AccountsPage;
