import { Settings, VpnKey } from "@mui/icons-material";
import { TabContext } from "@mui/lab";
import { Typography } from "@mui/material";
import { SyntheticEvent } from "react";
import GeneralSettingsContainer from "../../../application/components/GeneralSettings/GeneralSettingsContainer";
import Drawer from "../../components/Drawer/Drawer";
import strings from "../../localization/locales";

interface Props {
  tabsValue: string;
  onTabChange: (event: SyntheticEvent<Element, Event>, value: string) => void;
}

const ProfilePage: React.FC<Props> = ({ tabsValue, onTabChange }) => {
  return (
    <TabContext value={tabsValue}>
      <Drawer
        tabs={[
          {
            icon: <Settings />,
            label: <Typography>{strings.generalSettings}</Typography>,
            value: "0",
          },
          {
            icon: <VpnKey />,
            label: <Typography>{strings.securitySettings}</Typography>,
            value: "1",
          },
        ]}
        tabsValue={tabsValue}
        onTabChange={onTabChange}
      >
        <GeneralSettingsContainer />
      </Drawer>
    </TabContext>
  );
};

export default ProfilePage;
