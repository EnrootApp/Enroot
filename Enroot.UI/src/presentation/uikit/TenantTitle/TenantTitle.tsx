import { Typography } from "@mui/material";

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
      <Typography variant="subtitle1">{tenantName}</Typography>
      <Typography variant="subtitle1">{">"}</Typography>
      <Typography variant="h4">{title}</Typography>
    </div>
  );
};

export default TenantTitle;
