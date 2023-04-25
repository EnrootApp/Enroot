import {
  BaseQueryFn,
  FetchArgs,
  FetchBaseQueryError,
  createApi,
  fetchBaseQuery,
} from "@reduxjs/toolkit/query/react";
import { enqueueSnackbar } from "notistack";

const baseQuery = fetchBaseQuery({
  baseUrl: "https://localhost:7015",
  prepareHeaders: (headers) => {
    headers.set(
      "Authorization",
      `Bearer ${localStorage.getItem("accessToken")}`
    );
    headers.set(
      "Accept-Language",
      `${localStorage.getItem("lang")?.substring(2)}`
    );

    const pathname = window.location.pathname;
    const regex = /^\/tenant\/([^/]+)/;
    const match = pathname.match(regex);
    const tenantName = match?.[1] || "";

    if (match) {
      headers.set("Tenant", `${tenantName}`);
    }

    return headers;
  },
});

const baseQueryWithErrorHandlers: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);

  if (result.error && result.error.status === 403) {
    enqueueSnackbar("You don't have permission to do this", {
      variant: "error",
    });
  }

  if (
    (result.error && result.error.status === 401) ||
    result.error?.status === "FETCH_ERROR"
  ) {
    localStorage.removeItem("accessToken");
    //window.location.reload();
  }

  return result;
};

export const apiSlice = createApi({
  reducerPath: "api",
  baseQuery: baseQueryWithErrorHandlers,
  endpoints: () => ({}),
  tagTypes: ["Tasq", "Tasqs", "User"],
});
