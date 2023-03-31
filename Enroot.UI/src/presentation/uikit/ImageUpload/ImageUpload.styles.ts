import { IconButton, styled } from "@mui/material";

export const ButtonDiv = styled("div")(({ theme }) => ({
  position: "absolute",
  top: 0,
  bottom: 0,
  left: 0,
  right: 0,
  margin: "auto",
  display: "flex",
  height: "fit-content",
  justifyContent: "center",
}));

export const AvatarDiv = styled("div")(({ theme }) => ({
  position: "relative",
  "&:hover, &:active": {
    "& .icon": {
      opacity: 1,
    },

    "& .avatar": {
      opacity: 0.4,
    },
  },
}));

export const StyledIconButton = styled(IconButton)(({ theme }) => ({
  opacity: 0,
  transition: "0.2s",
}));
