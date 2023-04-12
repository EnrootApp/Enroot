import { Assessment, Assignment, Group } from "@mui/icons-material";
import {
  Divider,
  IconButton,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { routes } from "../../../infrastructure/routing/routes";
import { Home, Menu } from "@mui/icons-material";

import Link from "../../../presentation/uikit/Link/Link";
import strings from "../../localization/locales";
import { useState } from "react";
import { StyledList } from "./TenantNavigation.styles";

interface Props {
  hasGetReportPermission: boolean;
}

const TenantNavigation: React.FC<Props> = ({ hasGetReportPermission }) => {
  const [open, setOpen] = useState(false);

  return (
    <StyledList open={open}>
      <ListItem disablePadding sx={{ padding: 1 }}>
        <IconButton onClick={() => setOpen(!open)}>
          <Menu />
        </IconButton>
      </ListItem>

      <Divider />
      <ListItem disablePadding>
        <Link to={routes.tasqs} style={{ width: "100%" }}>
          <ListItemButton>
            <ListItemIcon>
              <Assignment />
            </ListItemIcon>
            <ListItemText primary={strings.tasqs} />
          </ListItemButton>
        </Link>
      </ListItem>
      <ListItem disablePadding>
        <Link to={routes.accounts} style={{ width: "100%" }}>
          <ListItemButton>
            <ListItemIcon>
              <Group />
            </ListItemIcon>
            <ListItemText primary={strings.employees} />
          </ListItemButton>
        </Link>
      </ListItem>
      {hasGetReportPermission && (
        <ListItem disablePadding>
          <Link to={routes.reports} style={{ width: "100%" }}>
            <ListItemButton>
              <ListItemIcon>
                <Assessment />
              </ListItemIcon>
              <ListItemText primary={strings.reports} />
            </ListItemButton>
          </Link>
        </ListItem>
      )}

      <Divider />
      <ListItem disablePadding>
        <Link to={routes.home} style={{ width: "100%" }}>
          <ListItemButton>
            <ListItemIcon>
              <Home />
            </ListItemIcon>
            <ListItemText primary={strings.home} />
          </ListItemButton>
        </Link>
      </ListItem>
    </StyledList>
  );
};

export default TenantNavigation;
