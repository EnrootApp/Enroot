import { Group } from "@mui/icons-material";
import { Typography } from "@mui/material";

interface UserAmountProps {
  amount: number;
}

const UserAmount: React.FC<UserAmountProps> = ({ amount }) => {
  return (
    <div style={{ display: "flex", gap: 8 }}>
      <Group />
      <Typography>{amount}</Typography>
    </div>
  );
};

export default UserAmount;
