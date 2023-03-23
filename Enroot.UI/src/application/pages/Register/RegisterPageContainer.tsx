import { FormikConfig } from "formik";
import React from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";

import RegisterPage from "../../../presentation/pages/Register/RegisterPage";
import { ISignUpForm } from "./RegisterPageContainer.types";

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .email(errorStrings.invalidEmail)
    .required(errorStrings.notEmpty),
  password: Yup.string()
    .required(errorStrings.notEmpty)
    .min(6, errorStrings.formatString(errorStrings.tooShort, 6).toString())
    .matches(new RegExp(/[a-z]/), errorStrings.characters),
});

const RegisterPageContainer: React.FC<{}> = () => {
  const formikConfig: FormikConfig<ISignUpForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { email: "", password: "" },
    onSubmit(values, formikHelpers) {},
  };

  return <RegisterPage formikConfig={formikConfig} />;
};

export default RegisterPageContainer;
