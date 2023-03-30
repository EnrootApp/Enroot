import { styled } from "@mui/material";
import { MaterialDesignContent } from "notistack";

export const StyledMaterialDesignContent = styled(MaterialDesignContent)(
  ({ theme }) => ({
    fontFamily: theme.typography.fontFamily,
  })
);
