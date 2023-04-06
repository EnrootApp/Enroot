import { Box, Container } from "@mui/material";
import { Outlet } from "react-router-dom";

import CircularProgressCentered from "../../uikit/CircularProgressCentered/CircularProgressCentered";
import HomeAppBarContainer from "../../../application/components/HomeAppBar/HomeAppBarContainer";
import TenantNavigation from "../../components/TenantNavigation/TenantNavigation";

interface Props {
  isLoading: boolean;
  tab: string;
  setTab: (tab: string) => void;
}

const TenantPage: React.FC<Props> = ({ isLoading }) => {
  return (
    <div style={{ width: "100%", height: "100%", overflow: "auto" }}>
      <HomeAppBarContainer />
      <Box
        sx={{
          m: 0,
          width: "100%",
          height: "100%",
          display: "flex",
          maxWidth: "100%",
        }}
      >
        <TenantNavigation />

        {isLoading ? <CircularProgressCentered /> : <Outlet />}
      </Box>
    </div>
  );
};

export default TenantPage;
