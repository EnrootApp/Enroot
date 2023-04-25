import { Box, styled } from "@mui/material";

export const Image = styled("img")(({}) => ({
  height: 150,
  width: 150,
  objectFit: "contain",
  borderRadius: "50%",
}));

export const StyledBox = styled(Box)(({}) => ({
  display: "flex",
  gap: 16,
  overflow: "auto",
  height: 250,
}));
