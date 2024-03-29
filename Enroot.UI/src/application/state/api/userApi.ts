import { AuthenticationResponse } from "../../../domain/authentication/AuthenticationResponse";
import { User } from "../../../domain/user/User";
import { ISignInForm } from "../../pages/Login/LoginPageContainer.types";
import { ISignUpForm } from "../../pages/Register/RegisterPageContainer.types";
import { apiSlice } from "./apiSlice";

export const userApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<AuthenticationResponse, ISignInForm>({
      query: ({ email, password }) => ({
        url: "/authentication/login",
        method: "POST",
        body: { email, password },
      }),
    }),
    register: builder.mutation<AuthenticationResponse, ISignUpForm>({
      query: ({ email, password }) => ({
        url: "/authentication/register",
        method: "POST",
        body: { email, password },
      }),
    }),
    forgotPassword: builder.query({
      query: ({ email }) => ({
        url: "/user/resetPasswordEmail",
        params: { email },
      }),
    }),
    resetPassword: builder.mutation({
      query: ({ code, newPassword, email }) => ({
        url: "/user/resetPassword",
        method: "POST",
        body: { code, newPassword, email },
      }),
    }),
    changePassword: builder.mutation({
      query: ({ currentPassword, newPassword }) => ({
        url: "/user/changePassword",
        method: "POST",
        body: { oldPassword: currentPassword, newPassword },
      }),
    }),
    getMe: builder.query<User, void>({
      query: () => ({
        url: "/user",
      }),
      providesTags: ["User"],
    }),
    updateInfo: builder.mutation({
      query: ({ firstName, lastName, avatarUrl }) => ({
        url: "/user",
        method: "PUT",
        body: { firstName, lastName, avatarUrl },
      }),
      invalidatesTags: ["User"],
    }),
  }),
});

export const {
  useLoginMutation,
  useRegisterMutation,
  useLazyForgotPasswordQuery,
  useResetPasswordMutation,
  useChangePasswordMutation,
  useGetMeQuery,
  useUpdateInfoMutation,
} = userApi;
