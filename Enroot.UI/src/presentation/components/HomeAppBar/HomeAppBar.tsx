import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import { HomeAppBarProps } from "../../../application/components/HomeAppBar/HomeAppBarContainer.types";
import strings from "../../localization/locales";
import Link from "../../uikit/Link/Link";
import { routes } from "../../../infrastructure/routing/routes";

const HomeAppBar: React.FC<HomeAppBarProps> = ({
  handleCloseUserMenu,
  handleOpenUserMenu,
  handleLogout,
  anchorElUser,
}) => {
  return (
    <AppBar position="static" sx={{ height: 70 }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar>DK</Avatar>
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: "45px" }}
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              <MenuItem>
                <Link to={routes.profile}>
                  <Typography textAlign="center">{strings.profile}</Typography>
                </Link>
              </MenuItem>
              <MenuItem onClick={handleLogout}>
                <Typography textAlign="center">{strings.logout}</Typography>
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default HomeAppBar;
