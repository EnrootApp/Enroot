import { styled } from "@mui/material";
import Button from "../../uikit/Button/Button";

export const StyledButton = styled(Button)(({ theme }) => ({
  flex: 1,
  [theme.breakpoints.down("md")]: {
    minWidth: "100%",
  },
}));
