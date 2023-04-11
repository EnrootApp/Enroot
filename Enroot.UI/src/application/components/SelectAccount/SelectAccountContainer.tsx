import { useEffect, useMemo, useState } from "react";
import SelectAccount from "../../../presentation/components/SelectAccount/SelectAccount";
import { debounce } from "@mui/material";
import { useLazyGetAccountsQuery } from "../../state/api/accountApi";
import { AccountModel } from "../../../domain/account/AccountModel";

interface Props {
  onChange: (value: string | undefined) => void;
  initial?: AccountModel;
}

const SelectAccountContainer: React.FC<Props> = ({
  onChange,
  initial = null,
}) => {
  const [search, setSearch] = useState<string>("");
  const [isEdit, setIsEdit] = useState(false);
  const [account, selectAccount] = useState<AccountModel | null>(initial);

  const [getAccounts, accounts] = useLazyGetAccountsQuery({});

  const debouncedSearch = useMemo(
    () =>
      debounce(() => {
        getAccounts({ name: search });
      }, 500),
    [search]
  );

  const onInputChange = (value: string | null) => {
    setSearch(value || "");
  };

  useEffect(() => {
    if (search.length) {
      debouncedSearch();
    }
    return () => debouncedSearch.clear();
  }, [search]);

  const onSelect = (value: AccountModel | null) => {
    selectAccount(value);
    onChange(value?.id);
    setIsEdit(false);
  };

  return (
    <SelectAccount
      accounts={accounts.data || []}
      isLoading={accounts.isLoading}
      onInputChange={onInputChange}
      onChange={onSelect}
      isEdit={isEdit}
      setIsEdit={setIsEdit}
      account={account}
    />
  );
};

export default SelectAccountContainer;
