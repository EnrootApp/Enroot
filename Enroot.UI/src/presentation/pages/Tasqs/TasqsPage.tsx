import { Tasq } from "../../../domain/tasq/Tasq";
import React from "react";
import {
  DataGrid,
  GridCallbackDetails,
  GridColDef,
  GridFilterModel,
  GridPaginationModel,
  enUS,
  ruRU,
} from "@mui/x-data-grid";
import { Box, Typography } from "@mui/material";
import TasqsToolbar from "../../components/TasqsToolbar/TasqsToolbar";
import User from "../../uikit/User/User";
import { Check, Clear } from "@mui/icons-material";
import strings from "../../localization/locales";
import Link from "../../uikit/Link/Link";

interface Props {
  tasqs: { tasqs: Tasq[]; totalAmount: number };
  isSuccess: boolean;
  onFilterModelChange: (
    model: GridFilterModel,
    details: GridCallbackDetails<"filter">
  ) => void;
  setPaginationModel: (model: GridPaginationModel) => void;
  paginationModel: GridPaginationModel;
}

const TasqsPage: React.FC<Props> = ({
  tasqs,
  isSuccess,
  onFilterModelChange,
  setPaginationModel,
  paginationModel,
}) => {
  const columns: GridColDef[] = [
    {
      field: "key",
      headerName: strings.key,
      flex: 0.2,
      sortable: false,
      filterable: false,
    },
    {
      field: "title",
      headerName: strings.summary,
      flex: 1,
      minWidth: 250,
      renderCell: (params) => (
        <Link to={`${params.row.key}`}>{params.value}</Link>
      ),
      sortable: false,
      filterable: false,
    },
    {
      field: "creator",
      headerName: strings.creator,
      flex: 1,
      minWidth: 200,
      renderCell: (params) => (
        <User imageSrc={params.value.avatarUrl} name={params.value.name} />
      ),
      sortable: false,
      filterable: false,
    },
    {
      field: "assignee",
      headerName: strings.assignee,
      renderCell: (params) => (
        <User
          imageSrc={params.value?.avatarUrl}
          name={params.value?.name || strings.emptyName}
        />
      ),
      flex: 1,
      minWidth: 200,
      sortable: false,
      filterable: false,
    },
    {
      field: "createdOn",
      headerName: strings.created,
      renderCell: (params) => (
        <Typography>{new Date(params.value).toLocaleString()}</Typography>
      ),
      flex: 1,
      minWidth: 200,
      sortable: false,
      filterable: false,
    },
    {
      field: "isCompleted",
      headerName: strings.completed,
      renderCell: (params) => (params.value === true ? <Check /> : <Clear />),
      flex: 0.5,
      sortable: false,
      type: "boolean",
    },
  ];

  return (
    <Box style={{ flex: 1 }}>
      <DataGrid
        getRowId={(row) => row.key}
        rows={tasqs?.tasqs || []}
        columns={columns}
        autoHeight
        loading={!isSuccess}
        disableRowSelectionOnClick
        paginationMode="server"
        filterMode="server"
        slots={{ toolbar: TasqsToolbar }}
        onFilterModelChange={onFilterModelChange}
        initialState={{
          pagination: {
            paginationModel: { pageSize: 25, page: 0 },
          },
        }}
        pageSizeOptions={[5, 10, 25]}
        onPaginationModelChange={setPaginationModel}
        paginationModel={paginationModel}
        rowCount={tasqs?.totalAmount || 0}
        localeText={
          localStorage.getItem("lang") === "enUS"
            ? enUS.components.MuiDataGrid.defaultProps.localeText
            : ruRU.components.MuiDataGrid.defaultProps.localeText
        }
      />
    </Box>
  );
};

export default TasqsPage;
