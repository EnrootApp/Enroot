import { FormikConfig } from "formik";
import TenantSettings from "../../../presentation/pages/TenantSettings/TenantSettings";
import {
  useDeleteTenantMutation,
  useGetCurrentTenantQuery,
  useUpdateTenantMutation,
} from "../../state/api/tenantApi";
import { useState } from "react";

const TenantSettingsContainer = () => {
  const [deleteTenant] = useDeleteTenantMutation();
  const [updateTenant] = useUpdateTenantMutation();
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const { data } = useGetCurrentTenantQuery({});

  const formikConfig: FormikConfig<{ imageSrc: string }> = {
    initialValues: { imageSrc: data?.logoUrl || "" },
    onSubmit: async (values) => {
      updateTenant({ logoUrl: values.imageSrc });
    },
  };

  return (
    <TenantSettings
      formikConfig={formikConfig}
      deleteTenant={() => deleteTenant({})}
      isDialogOpen={isDialogOpen}
      setIsDialogOpen={setIsDialogOpen}
    />
  );
};

export default TenantSettingsContainer;
