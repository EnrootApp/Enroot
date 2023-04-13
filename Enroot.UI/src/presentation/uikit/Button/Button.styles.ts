import { styled } from "@mui/material/styles";
import { Button, ButtonProps } from "@mui/material";

export const StyledButton = styled(Button)<ButtonProps>(
  ({ theme, variant, color }) => ({
    color: theme.palette.background.default,
    backgroundColor: color || theme.palette.secondary.main,
    textTransform: "none",

    "&:hover": {
      backgroundColor: theme.palette.secondary.light,
    },

    ...(variant == "outlined" && {
      border: `1px solid ${theme.palette.secondary.main}`,
      color: theme.palette.secondary.main,
      backgroundColor: theme.palette.background.default,

      "&:hover": {
        border: `1px solid ${theme.palette.secondary.dark}`,
        color: theme.palette.secondary.dark,
        backgroundColor: theme.palette.background.default,
      },
    }),
  })
);
