import { Box } from "@mui/material";
import TenantTitle from "../../uikit/TenantTitle/TenantTitle";
import strings from "../../localization/locales";
import { Formik, FormikConfig, FormikProps } from "formik";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import ImageUploadContainer from "../../../application/components/ImageUpload/ImageUploadContainer";
import Form from "../../uikit/Form/Form";
import Button from "../../uikit/Button/Button";
import { Delete } from "@mui/icons-material";
import { FormBox } from "./TenantSettings.styles";
import ConfirmationDialog from "../../components/ConfirmationDialog/ConfirmationDialog";

interface Props {
  formikConfig: FormikConfig<{ imageSrc: string }>;
  deleteTenant: () => void;
  isDialogOpen: boolean;
  setIsDialogOpen: (value: boolean) => void;
}

const TenantSettings: React.FC<Props> = ({
  formikConfig,
  deleteTenant,
  setIsDialogOpen,
  isDialogOpen,
}) => {
  return (
    <Box style={{ flex: 1 }}>
      <TenantTitle title={strings.settings} />
      <FormBox>
        <Formik {...formikConfig}>
          {(props: FormikProps<{ imageSrc: string }>) => {
            const { values, touched, setFieldValue, setFieldTouched } = props;
            return (
              <Form noValidate>
                <div style={{ display: "flex", alignItems: "center", gap: 16 }}>
                  <SubTitle value={strings.tenantImage} />
                  <ImageUploadContainer
                    setImageSrc={(imageSrc) => {
                      setFieldValue("imageSrc", imageSrc);
                      setFieldTouched("imageSrc", true);
                    }}
                    imageSrc={values.imageSrc}
                  />
                </div>
                <Button
                  sx={{ marginTop: 2, marginBottom: 2 }}
                  size="large"
                  type="submit"
                  disabled={!touched.imageSrc}
                >
                  {strings.save}
                </Button>
                <Button
                  sx={{ marginTop: 2, marginBottom: 2 }}
                  size="large"
                  color="error"
                  onClick={() => setIsDialogOpen(true)}
                >
                  {strings.deleteTenant}
                  <Delete />
                </Button>
              </Form>
            );
          }}
        </Formik>
      </FormBox>

      <ConfirmationDialog
        open={isDialogOpen}
        title={strings.deleteTenantConfirmation}
        onAgree={deleteTenant}
        onDisagree={() => {}}
        onClose={() => {
          setIsDialogOpen(false);
        }}
      />
    </Box>
  );
};

export default TenantSettings;
