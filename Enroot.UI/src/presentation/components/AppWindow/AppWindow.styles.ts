import { styled } from "@mui/material/styles";
import { Paper, Container, ContainerProps } from "@mui/material";
import { StyledContainerProps, StyledPaperProps } from "./AppWindow.types";

export const StyledPaper = styled(Paper, {
  shouldForwardProp: (prop) => prop !== "shrink",
})<StyledPaperProps>(({ theme, shrink }) => ({
  height: "80%",
  width: "100%",
  margin: "auto",
  display: "flex",
  padding: 0,

  ...(!shrink && {
    height: "100%",
  }),

  transition: "all 0.7s ease",

  [theme.breakpoints.down("md")]: {
    boxShadow: "none",
    height: "100%",
  },
}));

export const StyledContainer = styled(Container, {
  shouldForwardProp: (prop) => prop !== "shrink",
})<StyledContainerProps>(({ theme, shrink }) => ({
  display: "flex",
  height: "100%",
  padding: 0,

  transition: "all 0.7s ease",

  [theme.breakpoints.down("md")]: {
    padding: 0,
  },
  [theme.breakpoints.up("xs")]: {
    ...(!shrink && {
      maxWidth: "100%",
      padding: 0,
    }),
  },
}));
