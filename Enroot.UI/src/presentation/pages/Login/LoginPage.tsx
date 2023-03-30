import { Typography } from "@mui/material";
import { Formik, FormikConfig, FormikProps } from "formik";
import React from "react";
import { ISignInForm } from "../../../application/pages/Login/LoginPageContainer.types";
import AuthPaper from "../../uikit/AuthPaper/AuthPaper";
import Form from "../../uikit/Form/Form";
import PasswordInput from "../../uikit/PasswordInput/PasswordInput";
import strings from "../../localization/locales";
import { LinkBox } from "./LoginPage.styles";
import Button from "../../uikit/Button/Button";
import Link from "../../uikit/Link/Link";
import Input from "../../uikit/Input/Input";

interface Props {
  formikConfig: FormikConfig<ISignInForm>;
}

const LoginPage: React.FC<Props> = ({ formikConfig }) => {
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        {(props: FormikProps<ISignInForm>) => {
          const { values, touched, errors, handleBlur, handleChange } = props;

          return (
            <Form noValidate>
              <Typography variant="h4" align="center">
                {strings.signIn}
              </Typography>
              <Input
                label={strings.email}
                variant="standard"
                type="email"
                name="email"
                value={values.email}
                error={Boolean(errors.email && touched.email)}
                helperText={errors.email && touched.email && errors.email}
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
                  <Link to="/register">{strings.dontHaveAccount}</Link>
                </Typography>
                <Typography align="center">
                  <Link to="/forgotPassword">{strings.forgotPassword}</Link>
                </Typography>
              </LinkBox>
            </Form>
          );
        }}
      </Formik>
    </AuthPaper>
  );
};

export default LoginPage;
