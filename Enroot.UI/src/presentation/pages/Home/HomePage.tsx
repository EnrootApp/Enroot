import { Container, Grid } from "@mui/material";
import CreateTenantContainer from "../../../application/components/CreateTenant/CreateTenantContainer";
import HomeAppBarContainer from "../../../application/components/HomeAppBar/HomeAppBarContainer";
import TenantContainer from "../../../application/components/Tenant/TenantContainer";

interface Props {
  isSystemAdmin: boolean;
}

const HomePage: React.FC<Props> = ({ isSystemAdmin }) => {
  return (
    <div style={{ width: "100%", height: "100%", overflow: "auto" }}>
      <HomeAppBarContainer />
      <Container>
        <Grid
          container
          justifyContent="space-around"
          spacing={2}
          style={{ padding: 16, overflow: "hidden" }}
        >
          {isSystemAdmin && (
            <Grid item xs={12} sm={6} md={6} lg={4} xl={4}>
              <CreateTenantContainer />
            </Grid>
          )}

          {Array.from(new Array(12)).map(() => (
            <Grid item xs={12} sm={6} md={6} lg={4} xl={4}>
              <TenantContainer />
            </Grid>
          ))}
        </Grid>
      </Container>
    </div>
  );
};

export default HomePage;
