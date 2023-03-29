import { FormikConfig } from "formik";
import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";

import RegisterPage from "../../../presentation/pages/Register/RegisterPage";
import { useRegisterMutation } from "../../state/api/userApi";
import { ISignUpForm } from "./RegisterPageContainer.types";

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .matches(
      /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
      errorStrings.invalidEmail
    )
    .required(errorStrings.notEmpty),
  password: Yup.string()
    .required(errorStrings.notEmpty)
    .min(6, errorStrings.formatString(errorStrings.tooShort, 6).toString())
    .matches(new RegExp(/[a-z]/), errorStrings.characters),
});

const RegisterPageContainer: React.FC<{}> = () => {
  const [register, { isSuccess, data }] = useRegisterMutation();
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      localStorage.setItem("accessToken", data!.accessToken);
      navigate("/home");
    }
  }, [isSuccess]);

  const formikConfig: FormikConfig<ISignUpForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { email: "", password: "" },
    onSubmit: async (values) => {
      await register({ ...values });
    },
  };

  return <RegisterPage formikConfig={formikConfig} />;
};

export default RegisterPageContainer;
