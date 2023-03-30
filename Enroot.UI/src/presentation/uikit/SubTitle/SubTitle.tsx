import { Typography } from "@mui/material";
import { StyledBox } from "./SubTitle.styles";

interface Props {
  value: string;
}

const SubTitle: React.FC<Props> = ({ value }) => {
  return (
    <StyledBox>
      <Typography variant="h6">{value}</Typography>
    </StyledBox>
  );
};

export default SubTitle;
