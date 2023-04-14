import { ChangeEventHandler } from "react";
import LanguagePicker from "../../../presentation/components/LanguagePicker/LanguagePicker";

const LanguagePickerContainer = () => {
  const onChange: ChangeEventHandler<HTMLSelectElement> = (e) => {
    const value: string = e.target.value;
    localStorage.setItem("lang", value);
    window.location.reload();
  };

  return <LanguagePicker onChange={onChange} />;
};

export default LanguagePickerContainer;
