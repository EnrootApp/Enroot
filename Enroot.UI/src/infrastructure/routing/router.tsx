import { createBrowserRouter } from "react-router-dom";

import LoginPageContainer from "../../application/pages/Login/LoginPageContainer";
import ForgotPasswordPageContainer from "../../application/pages/ForgotPassword/ForgotPasswordPageContainer";
import RegisterPageContainer from "../../application/pages/Register/RegisterPageContainer";
import ResetPasswordPageContainer from "../../application/pages/ResetPassword/ResetPasswordPageContainer";
import ProtectedRoute from "./PrivateRoute";
import AnonymousRoute from "./AnonymousRoute";
import HomePageContainer from "../../application/pages/Home/HomePageContainer";
import AppWindowContainer from "../../application/components/AppWindow/AppWindowContainer";
import { routes } from "./routes";
import ProfilePageContainer from "../../application/pages/Profile/ProfilePageContainer";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AppWindowContainer />,
    children: [
      {
        path: routes.login,
        element: (
          <AnonymousRoute>
            <LoginPageContainer />
          </AnonymousRoute>
        ),
      },
      {
        path: routes.register,
        element: (
          <AnonymousRoute>
            <RegisterPageContainer />
          </AnonymousRoute>
        ),
      },
      {
        path: routes.forgotPassword,
        element: (
          <AnonymousRoute>
            <ForgotPasswordPageContainer />
          </AnonymousRoute>
        ),
      },
      {
        path: routes.resetPassword,
        element: (
          <AnonymousRoute>
            <ResetPasswordPageContainer />
          </AnonymousRoute>
        ),
      },
      {
        path: routes.home,
        element: (
          <ProtectedRoute>
            <HomePageContainer />
          </ProtectedRoute>
        ),
      },
      {
        path: routes.profile,
        element: (
          <ProtectedRoute>
            <ProfilePageContainer />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);
