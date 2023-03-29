import { isRejectedWithValue } from "@reduxjs/toolkit";
import type { MiddlewareAPI, Middleware } from "@reduxjs/toolkit";
import { enqueueSnackbar } from "notistack";

interface Errors {
  [key: string]: string[];
}

export const rtkQueryErrorLogger: Middleware =
  (api: MiddlewareAPI) => (next) => (action) => {
    if (isRejectedWithValue(action)) {
      console.warn(action);

      const errors = action.payload.data.errors as Errors;

      Object.entries(errors).forEach(
        ([key, descriptions]: [string, string[]]) => {
          descriptions.forEach((description: string) => {
            enqueueSnackbar(description, { variant: "error" });
          });
        }
      );
    }

    return next(action);
  };
