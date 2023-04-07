import { Autocomplete, Box } from "@mui/material";
import User from "../../uikit/User/User";
import Input from "../../uikit/Input/Input";
import strings from "../../localization/locales";
import { AccountModel } from "../../../domain/account/AccountModel";

interface Props {
  accounts: AccountModel[];
  onChange: (value: AccountModel | null) => void;
  onInputChange: (value: string | null) => void;
  isLoading: boolean;
}

const SelectAccount: React.FC<Props> = ({
  accounts,
  onChange,
  onInputChange,
  isLoading,
}) => {
  return (
    <Autocomplete
      filterOptions={(x) => x}
      options={accounts}
      renderOption={(props, account) => (
        <Box component="li" {...props} style={{ margin: 8 }}>
          <User imageSrc={account.avatarUrl} name={account.name} />
        </Box>
      )}
      getOptionLabel={(option) => option.name}
      renderInput={(params) => (
        <Input
          {...params}
          label={strings.selectUser}
          variant="standard"
          name="title"
        />
      )}
      onChange={(props, value) => onChange(value)}
      onInputChange={(props, value) => onInputChange(value)}
      loading={isLoading}
    />
  );
};

export default SelectAccount;
