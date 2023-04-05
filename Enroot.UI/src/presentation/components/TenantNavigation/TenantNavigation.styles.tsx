import { List, ListProps, styled } from "@mui/material";
import React from "react";

interface StyledListProps extends ListProps {
  open: boolean;
}

const CustomList: React.FC<StyledListProps> = (props) => {
  return <List {...props} />;
};

export const StyledList = styled(CustomList, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
  width: open ? 240 : 55,
  height: "100%",
  flexShrink: 0,
  whiteSpace: "nowrap",
  boxSizing: "border-box",
  overflow: "hidden",
  borderRight: `1px solid ${theme.palette.action.focus}`,

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
