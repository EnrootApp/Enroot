import { Language } from "@mui/icons-material";
import { FormControl, InputAdornment, NativeSelect } from "@mui/material";
import { ChangeEventHandler } from "react";

interface Props {
  onChange: ChangeEventHandler<HTMLSelectElement>;
}

const LanguagePicker: React.FC<Props> = ({ onChange }) => {
  return (
    <FormControl sx={{ width: "fit-content" }}>
      <NativeSelect
        defaultValue={localStorage.getItem("lang") || "ru"}
        startAdornment={
          <InputAdornment position="start">
            <Language />
          </InputAdornment>
        }
        disableUnderline
        onChange={onChange}
      >
        <option value="en">English</option>
        <option value="ru">Русский</option>
      </NativeSelect>
    </FormControl>
  );
};

export default LanguagePicker;
