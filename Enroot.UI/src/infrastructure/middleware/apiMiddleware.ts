import { isRejectedWithValue } from "@reduxjs/toolkit";
import type { MiddlewareAPI, Middleware } from "@reduxjs/toolkit";
import { enqueueSnackbar } from "notistack";

interface Errors {
  [key: string]: string[];
}

export const rtkQueryErrorLogger: Middleware =
  (api: MiddlewareAPI) => (next) => (action) => {
    if (isRejectedWithValue(action)) {
      const errors = action.payload?.data?.errors as Errors;

      if (!errors) {
        return;
      }

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
