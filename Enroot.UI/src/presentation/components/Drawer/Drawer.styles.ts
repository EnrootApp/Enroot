import styled from "@emotion/styled";
import { Drawer } from "@mui/material";

export const DrawerHeader = styled("div")(() => ({
  display: "flex",
  alignItems: "center",
  padding: "8px",
}));

export const StyledDrawer = styled(Drawer, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ open }) => ({
  width: open ? 240 : 55,
  flexShrink: 0,
  whiteSpace: "nowrap",
  boxSizing: "border-box",

  transition: "all 0.3s ease",

  "& .MuiPaper-root": {
    position: "relative",
  },
}));
