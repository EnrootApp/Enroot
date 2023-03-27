import SecuritySettings from "../../../presentation/components/SecuritySettings/SecuritySettings";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { SecuritySettingsForm } from "./SecuritySettingsContainer.types";
import { FormikConfig } from "formik";
import { useChangePasswordMutation } from "../../state/api/userApi";
import { enqueueSnackbar } from "notistack";
import apiStrings from "../../../presentation/localization/apiMessages";
import { useEffect } from "react";

const validationSchema = Yup.object().shape({
  currentPassword: Yup.string().required(errorStrings.notEmpty),
  newPassword: Yup.string()
    .required(errorStrings.notEmpty)
    .min(6, errorStrings.formatString(errorStrings.tooShort, 6).toString())
    .matches(new RegExp(/[a-z]/), errorStrings.characters),
});

const SecuritySettingsContainer = () => {
  const [changePassword, { isSuccess }] = useChangePasswordMutation();

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar(apiStrings.passwordChanged, { variant: "success" });
    }
  }, [isSuccess]);

  const formikConfig: FormikConfig<SecuritySettingsForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { currentPassword: "", newPassword: "" },
    onSubmit: async (values) => {
      await changePassword(values);
    },
  };

  return <SecuritySettings formikConfig={formikConfig} />;
};

export default SecuritySettingsContainer;
