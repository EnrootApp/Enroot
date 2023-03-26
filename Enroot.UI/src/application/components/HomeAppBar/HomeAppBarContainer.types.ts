import { MouseEventHandler } from "react";

export interface HomeAppBarProps {
  handleOpenUserMenu: MouseEventHandler;
  handleCloseUserMenu: MouseEventHandler;
  handleLogout: MouseEventHandler;
  anchorElUser: HTMLElement | null;
}
