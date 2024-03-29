import { Drawer, styled } from "@mui/material";

export const DrawerHeader = styled("div")(() => ({
  display: "flex",
  alignItems: "center",
  padding: "8px",
}));

export const StyledDrawer = styled(Drawer, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
  width: open ? 240 : 55,
  height: "100%",
  flexShrink: 0,
  whiteSpace: "nowrap",
  boxSizing: "border-box",

  transition: "all 0.3s ease",

  "& .MuiPaper-root": {
    overflow: "hidden",
    position: "relative",
  },

  backgroundColor: theme.palette.background.default,
  [theme.breakpoints.down("sm")]: {
    width: open ? "100%" : 55,
    position: open ? "absolute" : "relative",
    zIndex: 10,
  },
}));
