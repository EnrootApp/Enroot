import { useState } from "react";
import { useGetCurrentTenantQuery } from "../../state/api/tenantApi";
import TenantPage from "../../../presentation/pages/Tenant/TenantPage";
import { useGetMyAccountQuery } from "../../state/api/accountApi";

const TenantPageContainer = () => {
  const [tab, setTab] = useState("1");

  const { isLoading: isTenantLoading } = useGetCurrentTenantQuery(
    {},
    { refetchOnMountOrArgChange: true }
  );
  const { isLoading: isAccountLoading } = useGetMyAccountQuery(
    {},
    { refetchOnMountOrArgChange: true }
  );

  return (
    <TenantPage
      tab={tab}
      setTab={setTab}
      isLoading={isTenantLoading || isAccountLoading}
    />
  );
};

export default TenantPageContainer;
