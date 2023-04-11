import { AccountModel } from "../../../domain/account/AccountModel";
import { apiSlice } from "./apiSlice";
import { Me } from "../../../domain/account/Me";
import { AccountsFilters } from "../../pages/Accounts/AccountsPageContainer.types";

export const accountsApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getAccounts: builder.query<
      { accounts: AccountModel[]; totalAmount: number },
      AccountsFilters
    >({
      query: (filters) => ({
        url: "/account",
        params: filters,
      }),
    }),
    getMyAccount: builder.query<Me, {}>({
      query: () => ({
        url: "/account/me",
      }),
    }),
    setRole: builder.mutation<
      AccountModel,
      { accountId: string; roleId: string }
    >({
      query: (form) => ({
        url: "/account/role",
        method: "POST",
        body: { ...form },
      }),
    }),
  }),
});

export const {
  useLazyGetAccountsQuery,
  useGetMyAccountQuery,
  useSetRoleMutation,
} = accountsApi;
