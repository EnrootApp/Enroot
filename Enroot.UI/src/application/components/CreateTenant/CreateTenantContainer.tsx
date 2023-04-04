import { useEffect, useRef, useState } from "react";
import * as Yup from "yup";
import { FormikConfig } from "formik";

import { CreateTenantForm } from "./CreateTenantContainer.types";
import errorStrings from "../../../presentation/localization/errorMessages";
import {
  useCreateTenantMutation,
  useLazyGetTenantsQuery,
} from "../../state/api/tenantApi";
import CreateTenantCard from "../../../presentation/components/CreateTenantCard/CreateTenantCard";

const CreateTenantContainer = () => {
  const [isDialogOpen, setDialogOpen] = useState(false);
  const [createTenant, { isSuccess }] = useCreateTenantMutation();
  const [getTenants] = useLazyGetTenantsQuery();

  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .required(errorStrings.notEmpty)
      .matches(
        /^((?!-))[A-Za-z0-9][A-Za-z0-9-_]{2,61}[A-Za-z0-9]{0,1}$/,
        errorStrings.tenantName
      ),
  });

  const formikConfig: FormikConfig<CreateTenantForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { name: "", logoUrl: "" },
    onSubmit: async (values) => {
      await createTenant(values);
    },
  };

  useEffect(() => {
    if (isSuccess) {
      setDialogOpen(false);
      getTenants({ name: "" });
    }
  }, [isSuccess]);

  return (
    <CreateTenantCard
      onClick={() => setDialogOpen(true)}
      open={isDialogOpen}
      onClose={() => setDialogOpen(false)}
      formikConfig={formikConfig}
    />
  );
};

export default CreateTenantContainer;
