import React from "react";
import ReactDOM from "react-dom/client";
import "./presentation/styles/index.css";

import { ThemeProvider } from "@mui/material";
import { theme } from "./presentation/styles/theme";
import { RouterProvider } from "react-router-dom";
import { router } from "./infrastructure/routing/router";
import { Provider } from "react-redux";
import store from "./infrastructure/state/store";
import { SnackbarProvider } from "notistack";
import { StyledMaterialDesignContent } from "./presentation/components/Snackbar/Snackbar.styles";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <SnackbarProvider
          preventDuplicate
          autoHideDuration={5000}
          Components={{
            error: StyledMaterialDesignContent,
            success: StyledMaterialDesignContent,
            warning: StyledMaterialDesignContent,
            info: StyledMaterialDesignContent,
          }}
        />
        <RouterProvider router={router} />
      </ThemeProvider>
    </Provider>
  </React.StrictMode>
);
