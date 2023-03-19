import { Link, LinkProps } from "@mui/material";
import React from "react";

const Form: React.FC<LinkProps> = (props) => {
  return <Link {...props} underline="hover" />;
};

export default Form;
