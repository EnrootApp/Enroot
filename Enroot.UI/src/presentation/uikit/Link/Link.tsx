import React from "react";
import { LinkProps } from "react-router-dom";
import { StyledLink } from "./Link.styles";

const Link: React.FC<LinkProps> = (props) => {
  return <StyledLink {...props} />;
};

export default Link;
