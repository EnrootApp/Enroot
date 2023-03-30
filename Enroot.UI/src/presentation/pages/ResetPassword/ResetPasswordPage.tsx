import React from "react";
import { Typography } from "@mui/material";
import { Formik, FormikConfig, FormikProps } from "formik";
import { IResetPasswordForm } from "../../../application/pages/ResetPassword/ResetPasswordPageContainer.types";
import AuthPaper from "../../uikit/AuthPaper/AuthPaper";
import Form from "../../uikit/Form/Form";
import strings from "../../localization/locales";
import { LinkBox } from "./ResetPasswordPage.styles";
import PasswordInput from "../../uikit/PasswordInput/PasswordInput";
import { routes } from "../../../infrastructure/routing/routes";
import Link from "../../uikit/Link/Link";
import Button from "../../uikit/Button/Button";

interface Props {
  formikConfig: FormikConfig<IResetPasswordForm>;
}

const ResetPassword: React.FC<Props> = ({ formikConfig }) => {
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        {(props: FormikProps<IResetPasswordForm>) => {
          const { values, touched, errors, handleBlur, handleChange } = props;
          return (
            <Form noValidate>
              <Typography variant="h4" align="center">
                {strings.resetPassword}
              </Typography>
              <Typography align="center">
                {strings.resetPasswordGuide}
              </Typography>
              <PasswordInput
                label={strings.password}
                inputProps={{
                  name: "newPassword",
                  type: "password",
                  value: values.newPassword,
                  onChange: handleChange,
                  onBlur: handleBlur,
                }}
                error={Boolean(errors.newPassword && touched.newPassword)}
                helperText={
                  errors.newPassword && touched.newPassword
                    ? errors.newPassword
                    : ""
                }
              />
              <Button sx={{ marginTop: 2 }} size="large" type="submit">
                {strings.submit}
              </Button>
              <LinkBox>
                <Typography align="center">
                  <Link to={routes.login}>{strings.backToLogin}</Link>
                </Typography>
              </LinkBox>
            </Form>
          );
        }}
      </Formik>
    </AuthPaper>
  );
};

export default ResetPassword;
