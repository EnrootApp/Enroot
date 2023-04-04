import { Tenant } from "../../../domain/tenant/Tenant";
import TenantView from "../../../presentation/components/TenantCard/TenantCard";

interface Props {
  tenant: Tenant;
}

const TenantContainer: React.FC<Props> = ({ tenant }) => {
  return <TenantView onClick={() => {}} tenant={tenant} />;
};

export default TenantContainer;
