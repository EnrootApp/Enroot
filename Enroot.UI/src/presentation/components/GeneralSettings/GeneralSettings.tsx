import { TabPanel } from "@mui/lab";
import { Avatar } from "@mui/material";
import { Form, Formik, FormikConfig, FormikProps } from "formik";
import { ChangeEventHandler, MouseEventHandler, RefObject } from "react";
import { GeneralSettingsForm } from "../../../application/components/GeneralSettings/GeneralSettingsContainer.types";
import Button from "../../../presentation/components/Button/Button";
import Input from "../../../presentation/components/Input/Input";
import SubTitle from "../../../presentation/components/SubTitle/SubTitle";
import Title from "../../../presentation/components/Title/Title";
import strings from "../../../presentation/localization/locales";
import { Column } from "./GeneralSettings.styles";

interface Props {
  formikConfig: FormikConfig<GeneralSettingsForm>;
  fileInputRef: RefObject<HTMLInputElement>;
  handleFileChange: ChangeEventHandler<HTMLInputElement>;
  handleDeleteImage: MouseEventHandler<HTMLButtonElement>;
  imageUrl: string | null;
  file: File | null;
}

const GeneralSettings: React.FC<Props> = ({
  formikConfig,
  handleDeleteImage,
  handleFileChange,
  fileInputRef,
  imageUrl,
}) => {
  return (
    <TabPanel
      value="0"
      sx={{
        width: "100%",
        overflow: "auto",
        display: "flex",
        justifyContent: "space-between",
      }}
    >
      <Column>
        <Formik {...formikConfig}>
          {(props: FormikProps<GeneralSettingsForm>) => {
            const { values, touched, errors, handleBlur, handleChange } = props;
            return (
              <Form noValidate>
                <Title
                  value={strings.generalSettings + " " + strings.settings}
                />
                <div>
                  <SubTitle value={strings.profileImage} />
                  <div style={{ display: "flex" }}>
                    <input
                      accept="image/*"
                      type="file"
                      hidden
                      ref={fileInputRef}
                      onChange={(event) => handleFileChange(event)}
                    />
                    <Avatar
                      src={imageUrl || ""}
                      sx={{ width: 140, height: 140, marginRight: 4 }}
                    />
                    <div
                      style={{
                        display: "flex",
                        justifyContent: "space-evenly",
                        flexDirection: "column",
                        width: "100%",
                      }}
                    >
                      <Button
                        sx={{ width: "100%" }}
                        onClick={() => fileInputRef.current?.click()}
                      >
                        {strings.change}
                      </Button>
                      <Button
                        sx={{ width: "100%" }}
                        variant="outlined"
                        onClick={handleDeleteImage}
                      >
                        {strings.delete}
                      </Button>
                    </div>
                  </div>
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
                    variant="standard"
                    type="email"
                    name="email"
                    disabled
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

export default GeneralSettings;
