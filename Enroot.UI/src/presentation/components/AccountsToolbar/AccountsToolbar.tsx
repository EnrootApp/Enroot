import {
  GridToolbarColumnsButton,
  GridToolbarContainer,
  GridToolbarDensitySelector,
  GridToolbarQuickFilter,
} from "@mui/x-data-grid";
import strings from "../../localization/locales";
import TenantTitle from "../../uikit/TenantTitle/TenantTitle";
import AddTasqContainer from "../../../application/components/AddTasq/AddTasqContainer";
import { Permission } from "../../../application/common/enums/permission";
import { useGetMyAccountQuery } from "../../../application/state/api/accountApi";
import InviteAccountContainer from "../../../application/components/InviteAccount/InviteAccountContainer";

const AccountsToolbar = () => {
  const { data, isFetching } = useGetMyAccountQuery({});

  const showAddAccountButton =
    !isFetching && data?.permissions.includes(Permission.CreateAccount);

  return (
    <GridToolbarContainer sx={{ display: "flex", gap: 2 }}>
      <TenantTitle title={strings.employees} />

      <GridToolbarQuickFilter debounceMs={800} />
      <GridToolbarColumnsButton />
      <GridToolbarDensitySelector />
      {showAddAccountButton && <InviteAccountContainer />}
    </GridToolbarContainer>
  );
};

export default AccountsToolbar;
