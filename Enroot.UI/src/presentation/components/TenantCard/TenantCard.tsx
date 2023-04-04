import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import { CardActionArea } from "@mui/material";
import UserAmount from "../../uikit/UserAmount/UserAmount";
import { MouseEventHandler } from "react";
import { Tenant } from "../../../domain/tenant/Tenant";
import SubTitle from "../../uikit/SubTitle/SubTitle";

interface Props {
  onClick: MouseEventHandler<HTMLButtonElement>;
  tenant: Tenant;
}

const TenantCard: React.FC<Props> = ({ onClick, tenant }) => {
  return (
    <Card>
      <CardActionArea onClick={onClick}>
        <CardMedia
          component="img"
          height="200"
          alt={tenant.name}
          src={tenant.logoUrl || "imagePlaceholder.svg"}
        />
        <CardContent
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <SubTitle value={tenant.name} />
          <UserAmount amount={tenant.accountIds.length} />
        </CardContent>
      </CardActionArea>
    </Card>
  );
};

export default TenantCard;
