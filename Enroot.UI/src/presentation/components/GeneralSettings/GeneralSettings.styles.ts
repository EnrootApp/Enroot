import { styled } from "@mui/material";

export const Column = styled("div")(({ theme }) => ({
  width: "50%",
  [theme.breakpoints.down("md")]: {
    width: "100%",
  },
}));
