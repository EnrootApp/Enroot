import { useEffect, useState } from "react";
import { useGetCurrentTenantQuery } from "../../state/api/tenantApi";
import TenantPage from "../../../presentation/pages/Tenant/TenantPage";

const TenantPageContainer = () => {
  const [tab, setTab] = useState("1");

  const { data, isSuccess } = useGetCurrentTenantQuery({});

  useEffect(() => {
    if (isSuccess) {
      const tenantId = data?.id;
      localStorage.setItem("tenantId", tenantId || "");
    }
  }, [isSuccess]);

  return <TenantPage tab={tab} setTab={setTab} isLoading={!isSuccess} />;
};

export default TenantPageContainer;
