import { FormikConfig } from "formik";
import { enqueueSnackbar } from "notistack";
import React, { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import * as Yup from "yup";
import { routes } from "../../../infrastructure/routing/routes";
import apiStrings from "../../../presentation/localization/apiMessages";
import errorStrings from "../../../presentation/localization/errorMessages";

import ResetPassword from "../../../presentation/pages/ResetPassword/ResetPasswordPage";
import { useResetPasswordMutation } from "../../state/api/userApi";
import { IResetPasswordForm } from "./ResetPasswordPageContainer.types";

const validationSchema = Yup.object().shape({
  newPassword: Yup.string()
    .required(errorStrings.notEmpty)
    .min(6, errorStrings.formatString(errorStrings.tooShort, 6).toString())
    .matches(new RegExp(/[a-z]/), errorStrings.characters),
});

const ResetPasswordPageContainer: React.FC<{}> = () => {
  const [resetPassword, { isSuccess }] = useResetPasswordMutation();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const email = searchParams.get("email");
  const code = searchParams.get("code");

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar(apiStrings.passwordChanged, { variant: "success" });
      navigate(routes.login);
    }
  }, [isSuccess]);

  const formikConfig: FormikConfig<IResetPasswordForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { newPassword: "" },
    onSubmit: async (values) => {
      await resetPassword({ ...values, email, code });
    },
  };

  return <ResetPassword formikConfig={formikConfig} />;
};

export default ResetPasswordPageContainer;
