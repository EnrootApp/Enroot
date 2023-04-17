import { TabPanel } from "@mui/lab";
import { Formik, FormikConfig, FormikProps } from "formik";
import { GeneralSettingsForm } from "../../../application/components/GeneralSettings/GeneralSettingsContainer.types";
import ImageUploadContainer from "../../../application/components/FileUpload/FileUploadContainer";
import strings from "../../../presentation/localization/locales";
import Button from "../../uikit/Button/Button";
import Form from "../../uikit/Form/Form";
import Input from "../../uikit/Input/Input";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import Title from "../../uikit/Title/Title";
import { Column } from "./GeneralSettings.styles";

export interface Props {
  formikConfig: FormikConfig<GeneralSettingsForm>;
  email: string;
}

const GeneralSettings: React.FC<Props> = ({ formikConfig, email }) => {
  return (
    <TabPanel
      value="0"
      sx={{
        width: "100%",
        overflow: "auto",
      }}
    >
      <Column>
        <Formik {...formikConfig}>
          {(props: FormikProps<GeneralSettingsForm>) => {
            const {
              values,
              touched,
              errors,
              handleBlur,
              handleChange,
              setFieldValue,
              setFieldTouched,
            } = props;
            return (
              <Form noValidate>
                <Title value={strings.generalSettingsTitle} />
                <div>
                  <SubTitle value={strings.profileImage} />
                  <ImageUploadContainer
                    setImageSrc={(imageSrc) => {
                      setFieldValue("avatarUrl", imageSrc);
                      setFieldTouched("avatarUrl", true);
                    }}
                    imageSrc={values.avatarUrl}
                  />
                </div>

                <div
                  style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: 8,
                  }}
                >
                  <SubTitle value={strings.personalInfo} />
                  <Input
                    label={strings.firstName}
                    variant="standard"
                    name="firstName"
                    value={values.firstName}
                    error={Boolean(errors.firstName && touched.firstName)}
                    helperText={
                      errors.firstName && touched.firstName && errors.firstName
                    }
                    onChange={handleChange}
                    onBlur={handleBlur}
                  />
                  <Input
                    label={strings.lastName}
                    variant="standard"
                    name="lastName"
                    value={values.lastName}
                    error={Boolean(errors.lastName && touched.lastName)}
                    helperText={
                      errors.lastName && touched.lastName && errors.lastName
                    }
                    onChange={handleChange}
                    onBlur={handleBlur}
                  />
                  <Input
                    label={strings.email}
                    value={email}
                    variant="standard"
                    type="email"
                    name="email"
                    disabled
                  />
                  <Button
                    sx={{ marginTop: 2, marginBottom: 2 }}
                    size="large"
                    type="submit"
                    disabled={
                      !(
                        touched.lastName ||
                        touched.firstName ||
                        touched.avatarUrl
                      )
                    }
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

export default GeneralSettings;
