import { TabProps, Typography } from "@mui/material";
import { StyledTab } from "./Tab.styles";

const Tab: React.FC<TabProps> = (props) => {
  return <StyledTab iconPosition="start" {...props} />;
};

export default Tab;
