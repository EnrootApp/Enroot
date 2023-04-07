import { useEffect, useMemo, useState } from "react";
import SelectAccount from "../../../presentation/components/SelectAccount/SelectAccount";
import { debounce } from "@mui/material";
import { useLazyGetAccountsQuery } from "../../state/api/accountApi";
import { AccountModel } from "../../../domain/account/AccountModel";

interface Props {
  onChange: (value: string | undefined) => void;
}

const SelectAccountContainer: React.FC<Props> = ({ onChange }) => {
  const [search, setSearch] = useState<string>("");

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
    onChange(value?.id);
  };

  return (
    <SelectAccount
      accounts={accounts.data || []}
      isLoading={accounts.isLoading}
      onInputChange={onInputChange}
      onChange={onSelect}
    />
  );
};

export default SelectAccountContainer;
