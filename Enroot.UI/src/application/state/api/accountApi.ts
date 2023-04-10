import { AccountModel } from "../../../domain/account/AccountModel";
import { apiSlice } from "./apiSlice";
import { Permission } from "../../common/enums/permission";
import { Me } from "../../../domain/account/Me";

export const accountsApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getAccounts: builder.query<AccountModel[], { name: string }>({
      query: ({ name }) => ({
        url: "/account",
        params: { name },
      }),
    }),
    getMyAccount: builder.query<Me, {}>({
      query: () => ({
        url: "/account/me",
      }),
    }),
  }),
});

export const { useLazyGetAccountsQuery, useGetMyAccountQuery } = accountsApi;
