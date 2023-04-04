import { useNavigate } from "react-router-dom";
import { Tenant } from "../../../domain/tenant/Tenant";
import { routes } from "../../../infrastructure/routing/routes";
import TenantCard from "../../../presentation/components/TenantCard/TenantCard";

interface Props {
  tenant: Tenant;
}

const TenantContainer: React.FC<Props> = ({ tenant }) => {
  const navigate = useNavigate();

  return (
    <TenantCard
      onClick={() => {
        navigate(`${routes.tenant}/${tenant.name}`);
      }}
      tenant={tenant}
    />
  );
};

export default TenantContainer;
