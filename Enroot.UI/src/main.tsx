import React from "react";
import ReactDOM from "react-dom/client";
import "./presentation/styles/index.css";

import { ThemeProvider } from "@mui/material";
import { theme } from "./presentation/styles/theme";
import { Provider } from "react-redux";
import store from "./infrastructure/state/store";
import { SnackbarProvider } from "notistack";
import { StyledMaterialDesignContent } from "./presentation/uikit/Snackbar/Snackbar.styles";
import App from "./App";

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
        <App />
      </ThemeProvider>
    </Provider>
  </React.StrictMode>
);
