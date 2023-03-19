import { Link, Typography } from "@mui/material";
import { Box, Container } from "@mui/system";
import { Formik, FormikConfig } from "formik";
import React from "react";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import strings from "../../localization/locales";
import { LinkBox } from "./LoginPage.styles";

interface Props {
  formikConfig: FormikConfig<object>;
}

const LoginPage: React.FC<Props> = ({ formikConfig }) => {
  strings.setLanguage("en");
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        <Form>
          <Typography variant="h4" align="center">
            {strings.signIn}
          </Typography>
          <Input label={strings.email} variant="standard" type="email" />
          <Input label={strings.password} type="password" variant="standard" />
          <Button sx={{ marginTop: 2 }} size="large">
            {strings.submit}
          </Button>
          <LinkBox>
            <Typography align="center">
              <Link href="#" underline="hover">
                {strings.dontHaveAccount}
              </Link>
            </Typography>
            <Typography align="center">
              <Link href="#" underline="hover">
                {strings.forgotPassword}
              </Link>
            </Typography>
          </LinkBox>
        </Form>
      </Formik>
    </AuthPaper>
  );
};

export default LoginPage;
