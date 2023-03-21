import { createBrowserRouter } from "react-router-dom";

import ForgotPasswordPage from "../presentation/pages/ForgotPassword/ForgotPasswordPage";
import LoginPage from "../presentation/pages/Login/LoginPage";
import RegisterPage from "../presentation/pages/Register/RegisterPage";
import ResetPasswordPage from "../presentation/pages/ResetPassword/ResetPasswordPage";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: (
      <LoginPage
        formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
      />
    ),
  },
  {
    path: "/register",
    element: (
      <RegisterPage
        formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
      />
    ),
  },
  {
    path: "/forgotPassword",
    element: (
      <ForgotPasswordPage
        formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
      />
    ),
  },
  {
    path: "/resetPassword",
    element: (
      <ResetPasswordPage
        formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
      />
    ),
  },
]);
