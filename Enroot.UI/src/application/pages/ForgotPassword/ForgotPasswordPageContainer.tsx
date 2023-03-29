import { FormikConfig } from "formik";
import React from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import ForgotPassword from "../../../presentation/pages/ForgotPassword/ForgotPasswordPage";
import { useLazyForgotPasswordQuery } from "../../state/api/userApi";
import { IForgotPasswordForm } from "./ForgotPasswordPageContainer.types";

const ForgotPasswordPageContainer: React.FC<{}> = () => {
  const [forgotPassword] = useLazyForgotPasswordQuery({});

  const validationSchema = Yup.object().shape({
    email: Yup.string()
      .matches(
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        errorStrings.invalidEmail
      )
      .required(errorStrings.notEmpty),
  });

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
