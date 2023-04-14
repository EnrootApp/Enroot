import { ReactElement } from "react";
import { Navigate, useParams } from "react-router-dom";
import { routes } from "./routes";
import { Permission } from "../../application/common/enums/permission";
import { useGetMyAccountQuery } from "../../application/state/api/accountApi";

interface ProtectedRouteProps {
  children: ReactElement;
  permission?: Permission;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  children,
  permission,
}) => {
  const params = useParams();
  const skip = !Boolean(permission);

  const { data, isLoading } = useGetMyAccountQuery(
    {},
    {
      skip: skip,
    }
  );
  const authenticated = Boolean(localStorage.getItem("accessToken"));

  if (!authenticated) {
    return <Navigate to={routes.login} replace />;
  }

  if (permission && !isLoading && !data?.permissions.includes(permission)) {
    return <Navigate to={`${routes.tenant}/${params.name}`} replace />;
  }

  return children;
};
export default ProtectedRoute;
