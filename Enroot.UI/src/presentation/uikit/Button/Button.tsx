import { ButtonProps } from "@mui/material";
import React from "react";
import { StyledButton } from "./Button.styles";

const Button: React.FC<ButtonProps> = (props) => {
  return <StyledButton variant="contained" {...props} />;
};

export default Button;
