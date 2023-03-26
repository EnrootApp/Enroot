import { ContainerProps, PaperProps } from "@mui/material";

export interface StyledPaperProps extends PaperProps {
  shrink: boolean;
}

export interface StyledContainerProps extends ContainerProps {
  shrink: boolean;
}
