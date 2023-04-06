import { Tasq } from "../../../domain/tasq/Tasq";
import React from "react";
import {
  DataGrid,
  GridCallbackDetails,
  GridColDef,
  GridFilterModel,
  GridPaginationModel,
  getGridStringOperators,
} from "@mui/x-data-grid";
import { Box } from "@mui/material";
import TasqToolbar from "../../components/TasqToolbar/TasqToolbar";
import User from "../../uikit/User/User";
import { Check, Clear } from "@mui/icons-material";
import strings from "../../localization/locales";
import { routes } from "../../../infrastructure/routing/routes";
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

const containsFilterOperators = getGridStringOperators().filter(({ value }) =>
  ["contains"].includes(value)
);

const equalFilterOperators = getGridStringOperators().filter(({ value }) =>
  ["equals"].includes(value)
);

const columns: GridColDef[] = [
  {
    field: "key",
    headerName: "Key",
    flex: 0.2,
    sortable: false,
    filterable: false,
  },
  {
    field: "title",
    headerName: strings.summary,
    flex: 1,
    minWidth: 250,
    renderCell: (params) => <Link to={routes.home}>{params.value}</Link>,
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
    filterOperators: containsFilterOperators,
  },
  {
    field: "isCompleted",
    headerName: strings.completed,
    renderCell: (params) => (params.value === true ? <Check /> : <Clear />),
    flex: 0.5,
    sortable: false,
    type: "boolean",
  },
  {
    field: "isAssigned",
    headerName: strings.assigned,
    renderCell: (params) => (params.value === true ? <Check /> : <Clear />),
    flex: 0.5,
    sortable: false,
    type: "boolean",
  },
];

const TasqsPage: React.FC<Props> = ({
  tasqs,
  isSuccess,
  onFilterModelChange,
  setPaginationModel,
  paginationModel,
}) => {
  return (
    <Box sx={{ width: "100%" }}>
      <DataGrid
        getRowId={(row) => row.key}
        rows={tasqs?.tasqs || []}
        columns={columns}
        autoHeight
        loading={!isSuccess}
        disableRowSelectionOnClick
        paginationMode="server"
        filterMode="server"
        slots={{ toolbar: TasqToolbar }}
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
      />
    </Box>
  );
};

export default TasqsPage;
