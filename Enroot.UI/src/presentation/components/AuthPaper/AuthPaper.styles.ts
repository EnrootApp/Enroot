import { styled } from "@mui/material/styles";
import { Paper, PaperProps, Container, ContainerProps } from "@mui/material";

export const StyledPaper = styled(Paper)<PaperProps>(({ theme }) => ({
  height: "80%",
  width: "100%",
  margin: "auto",
  display: "flex",

  [theme.breakpoints.down("md")]: {
    boxShadow: "none",
    height: "100%",
  },
}));

export const StyledContainer = styled(Container)<ContainerProps>(
  ({ theme }) => ({
    display: "flex",
    height: "100%",

    [theme.breakpoints.down("md")]: {
      padding: 0,
    },
  })
);

export const FormContainer = styled(Container)<ContainerProps>(({ theme }) => ({
  height: "100%",
  padding: theme.spacing(0, 3),
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
}));

export const ImageContainer = styled(Container)<ContainerProps>(
  ({ theme }) => ({
    height: "100%",
    background: "url(/logo.png)",
    backgroundPosition: "center",
    backgroundSize: "cover",

    [theme.breakpoints.down("sm")]: {
      display: "none",
    },
  })
);
