import { Container } from "@mui/material";
import HomeAppBarContainer from "../../components/HomeAppBar/HomeAppBarContainer";

const TenantPageContainer = () => {
  return (
    <div style={{ width: "100%", height: "100%", overflow: "auto" }}>
      <HomeAppBarContainer />
      <Container></Container>
    </div>
  );
};

export default TenantPageContainer;
