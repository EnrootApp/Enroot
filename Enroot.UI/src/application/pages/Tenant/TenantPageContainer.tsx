import { Container, Drawer } from "@mui/material";
import HomeAppBarContainer from "../../components/HomeAppBar/HomeAppBarContainer";

const TenantPageContainer = () => {
  return (
    <div style={{ width: "100%", height: "100%", overflow: "auto" }}>
      <HomeAppBarContainer />
      <Container disableGutters>
        <Drawer
          sx={{
            width: 240,
            flexShrink: 0,
            whiteSpace: "nowrap",
            boxSizing: "border-box",
          }}
        ></Drawer>
      </Container>
    </div>
  );
};

export default TenantPageContainer;
