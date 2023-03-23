import { styled } from "@mui/material/styles";
import { Input, InputProps } from "@mui/material";

export const StyledInput = styled(Input)<InputProps>(({ theme }) => ({
  "&.Mui-focused fieldset": {
    borderColor: theme.palette.secondary.main,
  },
}));
