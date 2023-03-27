import { useState } from "react";
import ProfilePage from "../../../presentation/pages/Profile/ProfilePage";

const ProfilePageContainer = () => {
  const [tabsValue, setValue] = useState("0");

  const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
    setValue(newValue);
  };

  return <ProfilePage tabsValue={tabsValue} onTabChange={handleTabChange} />;
};

export default ProfilePageContainer;
