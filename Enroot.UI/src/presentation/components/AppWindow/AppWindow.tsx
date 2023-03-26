import React from "react";
import { Outlet } from "react-router-dom";
import { AppWindowProps } from "../../../application/components/AppWindow/AppWindowContainer.types";
import { StyledContainer, StyledPaper } from "./AppWindow.styles";

const AppWindow: React.FC<AppWindowProps> = ({ shrink }) => {
  return (
    <StyledContainer maxWidth="lg" shrink={shrink}>
      <StyledPaper elevation={10} shrink={shrink}>
        <Outlet />
      </StyledPaper>
    </StyledContainer>
  );
};

export default AppWindow;
