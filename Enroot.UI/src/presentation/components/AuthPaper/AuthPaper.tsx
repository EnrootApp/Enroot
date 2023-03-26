import { Fade, PaperProps } from "@mui/material";
import React from "react";
import { FormContainer, ImageContainer, StyledPaper } from "./AuthPaper.styles";

const AuthPaper: React.FC<PaperProps> = ({ children, ...props }) => {
  return (
    <StyledPaper elevation={0} {...props}>
      <Fade appear in={true} timeout={1000}>
        <FormContainer>
          {/*logo goes here*/}
          {children}
        </FormContainer>
      </Fade>
      <ImageContainer></ImageContainer>
    </StyledPaper>
  );
};

export default AuthPaper;
