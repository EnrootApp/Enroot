import * as React from "react";
import HomeAppBar from "../../../presentation/components/HomeAppBar/HomeAppBar";
import { HomeAppBarProps } from "./HomeAppBarContainer.types";

function HomeAppBarContainer() {
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleLogout = () => {
    localStorage.removeItem("accessToken");
    window.location.reload();
  };

  const props: HomeAppBarProps = {
    anchorElUser,
    handleCloseUserMenu,
    handleOpenUserMenu,
    handleLogout,
  };

  return <HomeAppBar {...props} />;
}
export default HomeAppBarContainer;
