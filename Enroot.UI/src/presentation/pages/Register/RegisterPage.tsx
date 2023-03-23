import { Typography } from "@mui/material";
import { Formik, FormikConfig, FormikProps } from "formik";
import React from "react";
import { ISignUpForm } from "../../../application/pages/Register/RegisterPageContainer.types";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import Link from "../../components/Link/Link";
import PasswordInput from "../../components/PasswordInput/PasswordInput";
import strings from "../../localization/locales";
import { LinkBox } from "./RegisterPage.styles";

interface Props {
  formikConfig: FormikConfig<ISignUpForm>;
}

const RegisterPage: React.FC<Props> = ({ formikConfig }) => {
  strings.setLanguage("en");
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        {(props: FormikProps<ISignUpForm>) => {
          const { values, touched, errors, handleBlur, handleChange } = props;
          return (
            <Form noValidate>
              <Typography variant="h4" align="center">
                {strings.signUp}
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
                  <Link to="/login">{strings.haveAccount}</Link>
                </Typography>
              </LinkBox>
            </Form>
          );
        }}
      </Formik>
    </AuthPaper>
  );
};

export default RegisterPage;
