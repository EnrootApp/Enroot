import { ReactElement } from "react";
import { Navigate } from "react-router-dom";
import { routes } from "./routes";

interface ProtectedRouteProps {
  children: ReactElement;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const authenticated = Boolean(localStorage.getItem("accessToken"));

  if (!authenticated) {
    return <Navigate to={routes.login} replace />;
  }
  return children;
};
export default ProtectedRoute;
