import React from "react";
import ReactDOM from "react-dom/client";
import "./presentation/styles/index.css";

import { ThemeProvider } from "@mui/material";
import { theme } from "./presentation/styles/theme";
import { RouterProvider } from "react-router-dom";
import { router } from "./infrastructure/router";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <RouterProvider router={router} />
    </ThemeProvider>
  </React.StrictMode>
);
