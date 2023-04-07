import { Avatar, Typography } from "@mui/material";
import { UserProps } from "./User.types";

const User: React.FC<UserProps> = ({ imageSrc, name }) => {
  return (
    <div style={{ display: "flex", alignItems: "center", gap: 8 }}>
      <Avatar src={imageSrc} /> <Typography>{name}</Typography>
    </div>
  );
};

export default User;
