import { Avatar, Typography } from "@mui/material";
import strings from "../../localization/locales";

interface Props {
  imageSrc: string;
  name: string;
}

const User: React.FC<Props> = ({ imageSrc, name }) => {
  return (
    <div style={{ display: "flex", alignItems: "center", gap: 8 }}>
      <Avatar src={imageSrc} /> <Typography>{name}</Typography>
    </div>
  );
};

export default User;
