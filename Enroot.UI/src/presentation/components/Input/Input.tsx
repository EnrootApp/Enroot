import { TextField, TextFieldProps } from "@mui/material";
import React from "react";

const Input: React.FC<TextFieldProps> = (props) => {
  return <TextField {...props} />;
};

export default Input;
