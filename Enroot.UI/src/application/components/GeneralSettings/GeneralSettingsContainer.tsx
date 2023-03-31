import { FormikConfig } from "formik";
import { useEffect } from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { GeneralSettingsForm } from "./GeneralSettingsContainer.types";
import GeneralSettings from "../../../presentation/components/GeneralSettings/GeneralSettings";
import { enqueueSnackbar } from "notistack";
import { useGetMeQuery, useUpdateInfoMutation } from "../../state/api/userApi";
import apiStrings from "../../../presentation/localization/apiMessages";

const GeneralSettingsContainer: React.FC<{}> = () => {
  const { data, isLoading } = useGetMeQuery();
  const [updateInfo, { isSuccess }] = useUpdateInfoMutation();

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar(apiStrings.settingsUpdated, { variant: "success" });
    }
  }, [isSuccess]);

  const validationSchema = Yup.object().shape({
    firstName: Yup.string().required(errorStrings.notEmpty).max(50),
    lastName: Yup.string().required(errorStrings.notEmpty).max(50),
  });

  const formikConfig: FormikConfig<GeneralSettingsForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: {
      firstName: data?.firstName || "",
      lastName: data?.lastName || "",
      avatarUrl: data?.avatarUrl || "",
    },
    onSubmit: (values) => {
      updateInfo(values);
    },
  };

  return !isLoading ? (
    <GeneralSettings formikConfig={formikConfig} email={data?.email || ""} />
  ) : null;
};

export default GeneralSettingsContainer;
