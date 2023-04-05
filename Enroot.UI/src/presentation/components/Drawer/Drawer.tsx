import { Home, Menu } from "@mui/icons-material";
import {
  Divider,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Tabs,
} from "@mui/material";
import { ReactElement, SyntheticEvent, useState } from "react";
import { routes } from "../../../infrastructure/routing/routes";
import strings from "../../localization/locales";
import Link from "../../uikit/Link/Link";
import Tab, { TabProps } from "../../uikit/Tab/Tab";
import { DrawerHeader, StyledDrawer } from "./Drawer.styles";

interface Props {
  tabs: TabProps[];
  children: ReactElement;
  tabsValue: string;
  onTabChange: (event: SyntheticEvent<Element, Event>, value: any) => void;
}

const Drawer: React.FC<Props> = ({
  tabs,
  children,
  tabsValue,
  onTabChange,
}) => {
  const [open, setOpen] = useState(false);

  return (
    <>
      <StyledDrawer variant="permanent" open={open}>
        <DrawerHeader>
          <IconButton onClick={() => setOpen(!open)}>
            <Menu />
          </IconButton>
        </DrawerHeader>

        <Divider />
        <Tabs
          orientation="vertical"
          value={tabsValue}
          onChange={onTabChange}
          sx={{
            borderRight: 1,
            borderColor: "divider",
          }}
        >
          {tabs.map((tab, index) => (
            <Tab {...tab} key={`vertical-tab-${index}`} />
          ))}
        </Tabs>
        <Divider />
        <List>
          <Link to={routes.home}>
            <ListItem disablePadding>
              <ListItemButton>
                <ListItemIcon>
                  <Home />
                </ListItemIcon>
                <ListItemText primary={strings.home} />
              </ListItemButton>
            </ListItem>
          </Link>
        </List>
      </StyledDrawer>
      {children}
    </>
  );
};

export default Drawer;
