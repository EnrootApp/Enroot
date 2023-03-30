import { Fade, PaperProps } from "@mui/material";
import React from "react";
import LanguagePickerContainer from "../../../application/components/LanguagePicker/LanguagePickerContainer";
import { FormContainer, ImageContainer, StyledPaper } from "./AuthPaper.styles";

const AuthPaper: React.FC<PaperProps> = ({ children, ...props }) => {
  return (
    <StyledPaper elevation={0} {...props}>
      <Fade appear in={true} timeout={1000}>
        <FormContainer>
          <div style={{ margin: "auto 0" }}>{children}</div>
          <div style={{ margin: "16px auto" }}>
            <LanguagePickerContainer />
          </div>
        </FormContainer>
      </Fade>
      <ImageContainer></ImageContainer>
    </StyledPaper>
  );
};

export default AuthPaper;
