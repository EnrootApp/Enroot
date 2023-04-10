import { Typography } from "@mui/material";
import Link from "../Link/Link";
import { routes } from "../../../infrastructure/routing/routes";

interface Props {
  title: string;
}

const TenantTitle: React.FC<Props> = ({ title }) => {
  const pathname = window.location.pathname;
  const regex = /^\/tenant\/([^/]+)/;
  const match = pathname.match(regex);
  const tenantName = match?.[1] || "";

  return (
    <div style={{ margin: 16, display: "flex", alignItems: "end", gap: 8 }}>
      <Link to={`${routes.tenant}/${tenantName}`}>
        <Typography variant="subtitle1">{tenantName}</Typography>
      </Link>
      <Typography variant="subtitle1">{">"}</Typography>
      <Typography variant="h4">{title}</Typography>
    </div>
  );
};

export default TenantTitle;
