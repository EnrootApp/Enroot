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
  isEdit: boolean;
  account: AccountModel | null;
  setIsEdit: (value: boolean) => void;
}

const SelectAccount: React.FC<Props> = ({
  accounts,
  onChange,
  onInputChange,
  isLoading,
  isEdit,
  setIsEdit,
  account,
}) => {
  return !isEdit ? (
    <div onClick={() => setIsEdit(true)}>
      <User
        imageSrc={account?.avatarUrl || ""}
        name={account?.name || strings.emptyName}
      />
    </div>
  ) : (
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
          autoFocus
        />
      )}
      onChange={(props, value) => onChange(value)}
      onInputChange={(props, value) => onInputChange(value)}
      loading={isLoading}
      noOptionsText={strings.noOptions}
      onBlur={() => setIsEdit(false)}
    />
  );
};

export default SelectAccount;
