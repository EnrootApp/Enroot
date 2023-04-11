import React from "react";
import {
  DataGrid,
  GridCallbackDetails,
  GridColDef,
  GridEventListener,
  GridFilterModel,
  GridPaginationModel,
  GridRowModel,
  enUS,
  ruRU,
} from "@mui/x-data-grid";
import { Avatar, Box } from "@mui/material";
import AccountsToolbar from "../../components/AccountsToolbar/AccountsToolbar";
import strings from "../../localization/locales";
import { AccountModel } from "../../../domain/account/AccountModel";
import { Role } from "../../../application/common/enums/role";

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
}

const AccountsPage: React.FC<Props> = ({
  accounts,
  isSuccess,
  onFilterModelChange,
  setPaginationModel,
  paginationModel,
  hasCreateAccountPermission,
  onRowEditCommit,
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

  return (
    <Box sx={{ width: "100%" }}>
      <DataGrid
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
          localStorage.getItem("lang") === "enUS"
            ? enUS.components.MuiDataGrid.defaultProps.localeText
            : ruRU.components.MuiDataGrid.defaultProps.localeText
        }
        slots={{ toolbar: AccountsToolbar }}
        disableRowSelectionOnClick
        processRowUpdate={(newRow, oldRow) => onRowEditCommit(newRow, oldRow)}
        onProcessRowUpdateError={(error) => console.log(error)}
      />
    </Box>
  );
};

export default AccountsPage;
