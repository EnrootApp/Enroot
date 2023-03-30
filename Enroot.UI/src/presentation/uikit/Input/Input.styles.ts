import { styled } from "@mui/material/styles";
import { TextField, TextFieldProps } from "@mui/material";

export const StyledInput = styled(TextField)<TextFieldProps>(({ theme }) => ({
  "&.Mui-focused fieldset": {
    borderColor: theme.palette.secondary.main,
  },
}));
