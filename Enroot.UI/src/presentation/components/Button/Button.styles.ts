import { styled } from "@mui/material/styles";
import { Button, ButtonProps } from "@mui/material";

export const StyledButton = styled(Button)<ButtonProps>(({ theme }) => ({
  color: theme.palette.background.default,
  backgroundColor: theme.palette.secondary.main,

  "&:hover": {
    backgroundColor: theme.palette.secondary.light,
  },
}));
