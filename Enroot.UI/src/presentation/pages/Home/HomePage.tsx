import { Container, Grid, Input } from "@mui/material";
import { ChangeEventHandler } from "react";
import CreateTenantContainer from "../../../application/components/CreateTenant/CreateTenantContainer";
import HomeAppBarContainer from "../../../application/components/HomeAppBar/HomeAppBarContainer";
import TenantContainer from "../../../application/components/Tenant/TenantContainer";
import { Tenant } from "../../../domain/tenant/Tenant";
import strings from "../../localization/locales";

interface Props {
  isSystemAdmin: boolean;
  tenants: Tenant[];
  setSearchName: ChangeEventHandler<HTMLInputElement>;
  searchName: string;
}

const HomePage: React.FC<Props> = ({
  isSystemAdmin,
  tenants,
  setSearchName,
  searchName,
}) => {
  return (
    <div style={{ width: "100%", height: "100%", overflow: "auto" }}>
      <HomeAppBarContainer />
      <Container>
        <div>
          <Input
            sx={{ width: "50%", m: 2 }}
            placeholder={strings.search}
            onChange={setSearchName}
            value={searchName}
          />
        </div>
        <Grid
          container
          justifyContent="start"
          spacing={2}
          style={{ padding: 16, overflow: "hidden" }}
        >
          {isSystemAdmin && (
            <Grid item xs={12} sm={6} md={6} lg={4} xl={4}>
              <CreateTenantContainer />
            </Grid>
          )}

          {tenants.map((tenant) => (
            <Grid item xs={12} sm={6} md={6} lg={4} xl={4} key={tenant.id}>
              <TenantContainer tenant={tenant} />
            </Grid>
          ))}
        </Grid>
      </Container>
    </div>
  );
};

export default HomePage;
