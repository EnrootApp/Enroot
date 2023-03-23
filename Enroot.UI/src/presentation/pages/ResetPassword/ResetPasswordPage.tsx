import React from "react";
import { Typography } from "@mui/material";
import { Formik, FormikConfig, FormikProps } from "formik";
import { IResetPasswordForm } from "../../../application/pages/ResetPassword/ResetPasswordPageContainer.types";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import Link from "../../components/Link/Link";
import strings from "../../localization/locales";
import { LinkBox } from "./ResetPasswordPage.styles";
import PasswordInput from "../../components/PasswordInput/PasswordInput";

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
              <Input
                label={strings.securityCode}
                variant="standard"
                type="text"
                name="securityCode"
                value={values.code}
                error={Boolean(errors.code && touched.code)}
                helperText={errors.code && touched.code && errors.code}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              <PasswordInput
                label={strings.password}
                inputProps={{
                  name: "password",
                  type: "password",
                  value: values.password,
                  onChange: handleChange,
                  onBlur: handleBlur,
                }}
                error={Boolean(errors.password && touched.password)}
                helperText={
                  errors.password && touched.password ? errors.password : ""
                }
              />
              <Button sx={{ marginTop: 2 }} size="large" type="submit">
                {strings.submit}
              </Button>
              <LinkBox>
                <Typography align="center">
                  <Link to="/login">{strings.backToLogin}</Link>
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
