import * as React from "react";
import { useNavigate } from "react-router-dom";
import { routes } from "../../../infrastructure/routing/routes";
import HomeAppBar from "../../../presentation/components/HomeAppBar/HomeAppBar";
import { HomeAppBarProps } from "./HomeAppBarContainer.types";

function HomeAppBarContainer() {
  const navigate = useNavigate();

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
    navigate(routes.login);
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
