import {
  GridToolbarColumnsButton,
  GridToolbarContainer,
  GridToolbarDensitySelector,
  GridToolbarQuickFilter,
} from "@mui/x-data-grid";
import strings from "../../localization/locales";
import TenantTitle from "../../uikit/TenantTitle/TenantTitle";

const TasqToolbar = () => {
  return (
    <GridToolbarContainer sx={{ display: "flex", gap: 2 }}>
      <TenantTitle title={strings.tasqs} />

      <GridToolbarQuickFilter debounceMs={800} />
      <GridToolbarColumnsButton />
      <GridToolbarDensitySelector />
    </GridToolbarContainer>
  );
};

export default TasqToolbar;
