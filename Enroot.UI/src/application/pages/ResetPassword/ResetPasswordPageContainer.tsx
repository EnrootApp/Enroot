import { FormikConfig } from "formik";
import React from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";

import ResetPassword from "../../../presentation/pages/ResetPassword/ResetPasswordPage";
import { IResetPasswordForm } from "./ResetPasswordPageContainer.types";

const validationSchema = Yup.object().shape({
  code: Yup.string().required(errorStrings.notEmpty),
  password: Yup.string()
    .required(errorStrings.notEmpty)
    .min(6, errorStrings.formatString(errorStrings.tooShort, 6).toString())
    .matches(new RegExp(/[a-z]/), errorStrings.characters),
});

const ResetPasswordPageContainer: React.FC<{}> = () => {
  const formikConfig: FormikConfig<IResetPasswordForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { code: "", password: "" },
    onSubmit(values, formikHelpers) {},
  };

  return <ResetPassword formikConfig={formikConfig} />;
};

export default ResetPasswordPageContainer;
