import { TabProps as MuiTabProps } from "@mui/material";
import { StyledTab } from "./Tab.styles";
import { ReactElement } from "react";

export interface TabProps extends MuiTabProps {
  component?: ReactElement;
}

const Tab: React.FC<TabProps> = (props) => {
  return <StyledTab iconPosition="start" {...props} />;
};

export default Tab;
