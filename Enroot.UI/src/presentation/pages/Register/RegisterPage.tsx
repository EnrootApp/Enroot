import { Typography } from "@mui/material";
import { Formik, FormikConfig } from "formik";
import React from "react";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import Link from "../../components/Link/Link";
import strings from "../../localization/locales";
import { LinkBox } from "./RegisterPage.styles";

interface Props {
  formikConfig: FormikConfig<object>;
}

const RegisterPage: React.FC<Props> = ({ formikConfig }) => {
  strings.setLanguage("en");
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        <Form>
          <Typography variant="h4" align="center">
            {strings.signUp}
          </Typography>
          <Input label={strings.email} variant="standard" type="email" />
          <Input label={strings.password} type="password" variant="standard" />
          <Button sx={{ marginTop: 2 }} size="large">
            {strings.submit}
          </Button>
          <LinkBox>
            <Typography align="center">
              <Link to="/login">{strings.haveAccount}</Link>
            </Typography>
          </LinkBox>
        </Form>
      </Formik>
    </AuthPaper>
  );
};

export default RegisterPage;
