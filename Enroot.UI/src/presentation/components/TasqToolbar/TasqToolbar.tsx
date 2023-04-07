import {
  GridToolbarColumnsButton,
  GridToolbarContainer,
  GridToolbarDensitySelector,
  GridToolbarQuickFilter,
} from "@mui/x-data-grid";
import strings from "../../localization/locales";
import TenantTitle from "../../uikit/TenantTitle/TenantTitle";
import AddTasqContainer from "../../../application/components/AddTasq/AddTasqContainer";
import { useGetPermissionsQuery } from "../../../application/state/api/accountApi";
import { Permission } from "../../../application/common/enums/permission";

const TasqToolbar = () => {
  const { data, isFetching } = useGetPermissionsQuery({});

  const showAddTasqButton =
    !isFetching && data?.includes(Permission.CreateTasq);

  return (
    <GridToolbarContainer sx={{ display: "flex", gap: 2 }}>
      <TenantTitle title={strings.tasqs} />

      <GridToolbarQuickFilter debounceMs={800} />
      <GridToolbarColumnsButton />
      <GridToolbarDensitySelector />
      {showAddTasqButton && <AddTasqContainer />}
    </GridToolbarContainer>
  );
};

export default TasqToolbar;
