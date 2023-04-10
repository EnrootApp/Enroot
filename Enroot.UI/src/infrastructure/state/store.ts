import { configureStore } from "@reduxjs/toolkit";
import { apiSlice } from "../../application/state/api/apiSlice";
import { rtkQueryErrorLogger } from "../middleware/apiMiddleware";
import componentSlice from "../../application/state/components/index";

export default configureStore({
  reducer: {
    [apiSlice.reducerPath]: apiSlice.reducer,
    components: componentSlice,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(apiSlice.middleware)
      .concat(rtkQueryErrorLogger),
});
