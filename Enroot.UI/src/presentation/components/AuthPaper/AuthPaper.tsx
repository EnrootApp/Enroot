import { Paper, PaperProps } from "@mui/material";
import { Container } from "@mui/system";
import React from "react";
import {
  FormContainer,
  ImageContainer,
  StyledContainer,
  StyledPaper,
} from "./AuthPaper.styles";

const AuthPaper: React.FC<PaperProps> = ({ children, ...props }) => {
  return (
    <StyledContainer maxWidth="lg">
      <StyledPaper elevation={10} {...props}>
        <FormContainer>
          {/*logo goes here*/}
          {children}
        </FormContainer>
        <ImageContainer></ImageContainer>
      </StyledPaper>
    </StyledContainer>
  );
};

export default AuthPaper;
