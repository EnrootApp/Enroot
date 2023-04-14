import * as React from "react";
import HomeAppBar from "../../../presentation/components/HomeAppBar/HomeAppBar";
import { HomeAppBarProps } from "./HomeAppBarContainer.types";
import { useDispatch } from "react-redux";
import { apiSlice } from "../../state/api/apiSlice";

function HomeAppBarContainer() {
  const dispatch = useDispatch();
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

    dispatch(apiSlice.util.resetApiState());
    window.location.href = "/login";
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
