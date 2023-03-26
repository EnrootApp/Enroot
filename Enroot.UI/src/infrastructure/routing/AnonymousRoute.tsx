import { ReactElement } from "react";
import { Navigate } from "react-router-dom";

interface AnonymousRouteProps {
  children: ReactElement;
}

const AnonymousRoute: React.FC<AnonymousRouteProps> = ({ children }) => {
  const authenticated = Boolean(localStorage.getItem("accessToken"));

  if (authenticated) {
    return <Navigate to="/home" replace />;
  }
  return children;
};
export default AnonymousRoute;
