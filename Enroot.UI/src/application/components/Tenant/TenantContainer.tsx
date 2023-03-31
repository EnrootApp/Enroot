import Tenant from "../../../presentation/components/TenantCard/TenantCard";

const TenantContainer = () => {
  return (
    <Tenant
      onClick={() => {}}
      tenant={{ name: "Tenant", id: "", accountIds: ["", ""], logoUrl: "" }}
    />
  );
};

export default TenantContainer;
