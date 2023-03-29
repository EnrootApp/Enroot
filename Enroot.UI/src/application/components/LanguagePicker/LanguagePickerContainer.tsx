import { ChangeEventHandler } from "react";
import { useDispatch } from "react-redux";
import LanguagePicker from "../../../presentation/components/LanguagePicker/LanguagePicker";

const LanguagePickerContainer = () => {
  const dispatch = useDispatch();
  const onChange: ChangeEventHandler<HTMLSelectElement> = (e) => {
    const value: string = e.target.value;
    localStorage.setItem("lang", value);
    window.location.reload();
  };

  return <LanguagePicker onChange={onChange} />;
};

export default LanguagePickerContainer;
