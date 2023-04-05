import { useEffect, useState } from "react";
import { useGetTenantsQuery } from "../../state/api/tenantApi";
import TenantPage from "../../../presentation/pages/Tenant/TenantPage";

const TenantPageContainer = () => {
  const [tab, setTab] = useState("1");
  const pathname = window.location.pathname;
  const regex = /^\/tenant\/([^/]+)/;
  const match = pathname.match(regex);
  const tenantName = match?.[1] || "";

  const { data, isSuccess } = useGetTenantsQuery({ name: tenantName });

  useEffect(() => {
    if (isSuccess) {
      const tenantId = data?.find((tenant) => tenant.name === tenantName)?.id;
      localStorage.setItem("tenantId", tenantId || "");
    }
  }, [isSuccess]);

  return <TenantPage tab={tab} setTab={setTab} isLoading={!isSuccess} />;
};

export default TenantPageContainer;
