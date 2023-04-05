import { Assignment, AssignmentInd } from "@mui/icons-material";
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

const TenantNavigation: React.FC<{}> = () => {
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
        <Link to={routes.assignments} style={{ width: "100%" }}>
          <ListItemButton>
            <ListItemIcon>
              <AssignmentInd />
            </ListItemIcon>
            <ListItemText primary={strings.assignments} />
          </ListItemButton>
        </Link>
      </ListItem>
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
