import { AccountModel } from "../../../domain/account/AccountModel";
import { apiSlice } from "./apiSlice";
import { Me } from "../../../domain/account/Me";
import { AccountsFilters } from "../../pages/Accounts/AccountsPageContainer.types";
import { InviteAccountForm } from "../../components/InviteAccount/InviteAccountContainer.types";

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
    deleteAccount: builder.mutation<{}, { id: string }>({
      query: ({ id }) => ({
        url: `/account/${id}`,
        method: "DELETE",
      }),
    }),
    restoreAccount: builder.mutation<{}, { id: string }>({
      query: ({ id }) => ({
        url: `/account/${id}`,
        method: "POST",
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
    inviteAccount: builder.mutation<{}, InviteAccountForm>({
      query: (form) => ({
        url: `/account/invite`,
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
  useDeleteAccountMutation,
  useInviteAccountMutation,
  useLazyGetMyAccountQuery,
  useRestoreAccountMutation,
} = accountsApi;
