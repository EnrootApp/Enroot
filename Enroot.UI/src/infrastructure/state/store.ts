import { configureStore } from "@reduxjs/toolkit";
import { apiSlice } from "../../application/state/api/apiSlice";
import { rtkQueryErrorLogger } from "../middleware/apiMiddleware";

export default configureStore({
  reducer: {
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(apiSlice.middleware)
      .concat(rtkQueryErrorLogger),
});
