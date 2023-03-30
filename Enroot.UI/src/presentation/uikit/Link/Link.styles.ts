import { Link, LinkProps } from "react-router-dom";

import { styled } from "@mui/material/styles";

export const StyledLink = styled(Link)<LinkProps>(({ theme }) => ({
  textDecoration: "none",
  color: "inherit",
  "&:hover": {
    textDecoration: "underline",
  },
}));
