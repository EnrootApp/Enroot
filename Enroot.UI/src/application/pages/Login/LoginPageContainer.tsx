import { FormikConfig } from "formik";
import React from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";

import LoginPage from "../../../presentation/pages/Login/LoginPage";
import { useLoginMutation } from "../../state/api/userApi";
import { ISignInForm } from "./LoginPageContainer.types";

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .email(errorStrings.invalidEmail)
    .required(errorStrings.notEmpty),
  password: Yup.string().required(errorStrings.notEmpty),
});

const LoginPageContainer: React.FC<{}> = () => {
  const [login] = useLoginMutation();

  const formikConfig: FormikConfig<ISignInForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { email: "", password: "" },
    onSubmit: async (values, formikHelpers) => {
      var res = await login({ ...values });
    },
  };

  return <LoginPage formikConfig={formikConfig} />;
};

export default LoginPageContainer;
