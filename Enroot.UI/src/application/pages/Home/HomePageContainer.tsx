import { debounce } from "@mui/material";
import { useEffect, useMemo, useState } from "react";
import HomePage from "../../../presentation/pages/Home/HomePage";
import { useLazyGetTenantsQuery } from "../../state/api/tenantApi";
import { useGetMeQuery } from "../../state/api/userApi";

const HomePageContainer = () => {
  const { data, isFetching } = useGetMeQuery();
  const [getTenants, tenants] = useLazyGetTenantsQuery({});
  const [searchName, setSearchName] = useState("");

  const debouncedSearch = useMemo(
    () =>
      debounce(() => {
        getTenants({ name: searchName });
      }, 500),
    [searchName]
  );

  useEffect(() => {
    debouncedSearch();
    return () => debouncedSearch.clear();
  }, [searchName]);

  const isSystemAdmin = !isFetching && data?.role === "SystemAdmin";

  return tenants.isFetching ? null : (
    <HomePage
      isSystemAdmin={isSystemAdmin}
      tenants={tenants.data || []}
      setSearchName={(event) => setSearchName(event.target.value)}
      searchName={searchName}
    />
  );
};

export default HomePageContainer;
