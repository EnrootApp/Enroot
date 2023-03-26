import { FormikConfig } from "formik";
import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";

import LoginPage from "../../../presentation/pages/Login/LoginPage";
import { useLoginMutation } from "../../state/api/userApi";
import { ISignInForm } from "./LoginPageContainer.types";

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .matches(
      /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
      errorStrings.invalidEmail
    )
    .required(errorStrings.notEmpty),
  password: Yup.string().required(errorStrings.notEmpty),
});

const LoginPageContainer: React.FC<{}> = () => {
  const [login, { isSuccess, data }] = useLoginMutation();
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      localStorage.setItem("accessToken", data!.accessToken);
      navigate("/home");
    }
  }, [isSuccess]);

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
