import { Typography } from "@mui/material";
import { StyledBox } from "./Title.styles";

interface Props {
  value: string;
}

const Title: React.FC<Props> = ({ value }) => {
  return (
    <StyledBox>
      <Typography variant="h5">{value}</Typography>
    </StyledBox>
  );
};

export default Title;
