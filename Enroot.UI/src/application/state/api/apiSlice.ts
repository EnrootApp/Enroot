import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const apiSlice = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7015",
    prepareHeaders: (headers) => {
      headers.set(
        "Authorization",
        `Bearer ${localStorage.getItem("accessToken")}`
      );
      headers.set("Accept-Language", `Bearer ${localStorage.getItem("lang")}`);
      headers.set("TenantId", `${localStorage.getItem("tenantId")}`);
      return headers;
    },
  }),
  endpoints: () => ({}),
});
