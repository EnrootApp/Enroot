import TenantNavigation from "../../../presentation/components/TenantNavigation/TenantNavigation";
import { Permission } from "../../common/enums/permission";
import { useGetMyAccountQuery } from "../../state/api/accountApi";

const TenantNavigationContainer: React.FC<{}> = () => {
  const { data: me, isLoading } = useGetMyAccountQuery({});
  const hasGetReportPermission =
    me?.permissions.includes(Permission.GetReport) || false;
  const hasModifyTenantSettingsPermission =
    me?.permissions.includes(Permission.ModifyTenantSettings) || false;

  return (
    <TenantNavigation
      hasGetReportPermission={hasGetReportPermission}
      hasModifyTenantSettingsPermission={hasModifyTenantSettingsPermission}
    />
  );
};

export default TenantNavigationContainer;
