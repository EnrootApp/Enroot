import { Typography } from "@mui/material";
import { Formik, FormikConfig, FormikProps } from "formik";
import React from "react";
import { IForgotPasswordForm } from "../../../application/pages/ForgotPassword/ForgotPasswordPageContainer.types";
import { routes } from "../../../infrastructure/routing/routes";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import Link from "../../components/Link/Link";
import strings from "../../localization/locales";
import { LinkBox } from "./ForgotPasswordPage.styles";

interface Props {
  formikConfig: FormikConfig<IForgotPasswordForm>;
}

const ForgotPassword: React.FC<Props> = ({ formikConfig }) => {
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        {(props: FormikProps<IForgotPasswordForm>) => {
          const { values, touched, errors, handleBlur, handleChange } = props;

          return (
            <Form noValidate>
              <Typography variant="h4" align="center">
                {strings.forgotPassword}
              </Typography>
              <Typography align="center">
                {strings.forgotPasswordGuide}
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

export default ForgotPassword;
