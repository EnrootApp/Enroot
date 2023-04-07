import { AccountModel } from "../../../domain/account/AccountModel";
import { apiSlice } from "./apiSlice";
import { Permission } from "../../common/enums/permission";

export const accountsApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getAccounts: builder.query<AccountModel[], { name: string }>({
      query: ({ name }) => ({
        url: "/account",
        params: { name },
      }),
    }),
    getPermissions: builder.query<Permission[], {}>({
      query: () => ({
        url: "/account/permissions",
      }),
    }),
  }),
});

export const { useLazyGetAccountsQuery, useGetPermissionsQuery } = accountsApi;
