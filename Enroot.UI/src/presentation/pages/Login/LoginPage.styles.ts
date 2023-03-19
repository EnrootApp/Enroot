import { Box, BoxProps } from "@mui/material";
import { styled } from "@mui/material/styles";

export const LinkBox = styled(Box)<BoxProps>(({ theme }) => ({
  display: "flex",
  justifyContent: "space-between",
  padding: 0,
  gap: theme.spacing(),
}));
