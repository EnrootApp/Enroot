import { apiSlice } from "./apiSlice";

// Define our single API slice object
export const userApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation({
      query: ({ email, password }) => ({
        url: "/authentication/login",
        method: "POST",
        body: { email, password },
      }),
    }),
    register: builder.mutation({
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
  }),
});

export const {
  useLoginMutation,
  useRegisterMutation,
  useLazyForgotPasswordQuery,
  useResetPasswordMutation,
} = userApi;
