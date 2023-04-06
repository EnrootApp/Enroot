import { createTheme, PaletteOptions } from "@mui/material";
import { enUS, ruRU } from "@mui/x-data-grid";

declare module "@mui/material/styles" {}

export const myPalette: PaletteOptions = {
  primary: {
    main: "#374151",
  },
  secondary: {
    main: "#2a9df4",
    light: "#3aa9fc",
  },
  background: {
    paper: "#fff",
    default: "#fff",
  },
};

export const theme = createTheme(
  {
    typography: {
      fontFamily: ["Lato"].join(","),
    },
    palette: myPalette,
  },
  ruRU,
  enUS
);
