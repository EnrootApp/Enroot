import { Link, Typography } from "@mui/material";
import { Formik, FormikConfig } from "formik";
import React from "react";
import AuthPaper from "../../components/AuthPaper/AuthPaper";
import Button from "../../components/Button/Button";
import Form from "../../components/Form/Form";
import Input from "../../components/Input/Input";
import strings from "../../localization/locales";
import { LinkBox } from "./ResetPassword.styles";

interface Props {
  formikConfig: FormikConfig<object>;
}

const ResetPassword: React.FC<Props> = ({ formikConfig }) => {
  strings.setLanguage("en");
  return (
    <AuthPaper>
      <Formik {...formikConfig}>
        <Form>
          <Typography variant="h4" align="center">
            {strings.resetPassword}
          </Typography>
          <Typography align="center">{strings.resetPasswordGuide}</Typography>
          <Input label={strings.securityCode} variant="standard" type="text" />
          <Input label={strings.password} variant="standard" type="password" />
          <Button sx={{ marginTop: 2 }} size="large">
            {strings.submit}
          </Button>
          <LinkBox>
            <Typography align="center">
              <Link href="#" underline="hover">
                {strings.backToLogin}
              </Link>
            </Typography>
          </LinkBox>
        </Form>
      </Formik>
    </AuthPaper>
  );
};

export default ResetPassword;
