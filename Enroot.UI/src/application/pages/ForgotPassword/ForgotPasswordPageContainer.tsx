import { FormikConfig } from "formik";
import React from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import ForgotPassword from "../../../presentation/pages/ForgotPassword/ForgotPasswordPage";
import { useLazyForgotPasswordQuery } from "../../state/api/userApi";
import { IForgotPasswordForm } from "./ForgotPasswordPageContainer.types";

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .email(errorStrings.invalidEmail)
    .required(errorStrings.notEmpty),
});

const ForgotPasswordPageContainer: React.FC<{}> = () => {
  const [forgotPassword] = useLazyForgotPasswordQuery({});

  const formikConfig: FormikConfig<IForgotPasswordForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { email: "" },
    onSubmit: async (values, formikHelpers) => {
      await forgotPassword({ ...values });
    },
  };

  return <ForgotPassword formikConfig={formikConfig} />;
};

export default ForgotPasswordPageContainer;
