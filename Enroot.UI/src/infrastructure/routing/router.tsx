import { createBrowserRouter } from "react-router-dom";

import LoginPageContainer from "../../application/pages/Login/LoginPageContainer";
import ForgotPasswordPageContainer from "../../application/pages/ForgotPassword/ForgotPasswordPageContainer";
import RegisterPageContainer from "../../application/pages/Register/RegisterPageContainer";
import ResetPasswordPageContainer from "../../application/pages/ResetPassword/ResetPasswordPageContainer";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPageContainer />,
  },
  {
    path: "/register",
    element: <RegisterPageContainer />,
  },
  {
    path: "/forgotPassword",
    element: <ForgotPasswordPageContainer />,
  },
  {
    path: "/resetPassword",
    element: <ResetPasswordPageContainer />,
  },
]);
