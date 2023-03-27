import { TabPanel } from "@mui/lab";
import { Formik, FormikConfig, FormikProps } from "formik";
import { SecuritySettingsForm } from "../../../application/components/SecuritySettings/SecuritySettingsContainer.types";
import strings from "../../localization/locales";
import Button from "../Button/Button";
import Form from "../Form/Form";
import { Column } from "../GeneralSettings/GeneralSettings.styles";
import PasswordInput from "../PasswordInput/PasswordInput";
import SubTitle from "../SubTitle/SubTitle";
import Title from "../Title/Title";

interface Props {
  formikConfig: FormikConfig<SecuritySettingsForm>;
}

const SecuritySettings: React.FC<Props> = ({ formikConfig }) => {
  return (
    <TabPanel
      value="1"
      sx={{
        width: "100%",
        overflow: "auto",
      }}
    >
      <Column>
        <Formik {...formikConfig}>
          {(props: FormikProps<SecuritySettingsForm>) => {
            const { values, touched, errors, handleBlur, handleChange } = props;
            return (
              <Form noValidate>
                <Title value={strings.securitySettingsTitle} />
                <div
                  style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: 8,
                  }}
                >
                  <SubTitle value={strings.password} />
                  <PasswordInput
                    label={strings.currentPassword}
                    inputProps={{
                      name: "currentPassword",
                      type: "password",
                      value: values.currentPassword,
                      onChange: handleChange,
                      onBlur: handleBlur,
                    }}
                    error={Boolean(
                      errors.currentPassword && touched.currentPassword
                    )}
                    helperText={
                      errors.currentPassword && touched.currentPassword
                        ? errors.currentPassword
                        : ""
                    }
                  />
                  <PasswordInput
                    label={strings.newPassword}
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
                  <Button
                    sx={{ marginTop: 2, marginBottom: 2 }}
                    size="large"
                    type="submit"
                  >
                    {strings.save}
                  </Button>
                </div>
              </Form>
            );
          }}
        </Formik>
      </Column>
    </TabPanel>
  );
};

export default SecuritySettings;
