import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import {
  CardActionArea,
  Container,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import { MouseEventHandler } from "react";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import strings from "../../localization/locales";
import Dialog from "../../uikit/Dialog/Dialog";
import { Formik, FormikConfig, FormikProps } from "formik";
import Form from "../../uikit/Form/Form";
import Button from "../../uikit/Button/Button";
import { CreateTenantForm } from "../../../application/components/CreateTenant/CreateTenantContainer.types";
import Input from "../../uikit/Input/Input";
import ImageUploadContainer from "../../../application/components/ImageUpload/ImageUploadContainer";
import { Column } from "../GeneralSettings/GeneralSettings.styles";
import Title from "../../uikit/Title/Title";

interface Props {
  open: boolean;
  onClick: MouseEventHandler<HTMLButtonElement>;
  onClose: () => void;
  formikConfig: FormikConfig<CreateTenantForm>;
}

const CreateTenantCard: React.FC<Props> = ({
  onClick,
  open,
  onClose,
  formikConfig,
}) => {
  return (
    <>
      <Card>
        <CardActionArea onClick={onClick}>
          <CardMedia component="img" height="200" src="createTenant.png" />
          <CardContent
            sx={{
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <SubTitle value={strings.createTenant} />
          </CardContent>
        </CardActionArea>
      </Card>
      <Dialog
        dialogProps={{
          fullWidth: true,
          maxWidth: "sm",
          open: open,
          onClose: onClose,
        }}
        dialogContent={
          <>
            <DialogTitle>
              <SubTitle value={strings.createTenant} />
            </DialogTitle>
            <DialogContent>
              <Formik {...formikConfig}>
                {(props: FormikProps<CreateTenantForm>) => {
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
                      <div
                        style={{ display: "flex", justifyContent: "center" }}
                      >
                        <ImageUploadContainer
                          imageSrc={values.logoUrl}
                          setImageSrc={(imageSrc) => {
                            setFieldValue("logoUrl", imageSrc);
                            setFieldTouched("logoUrl", true);
                          }}
                        />
                      </div>
                      <Input
                        label={strings.tenantName}
                        variant="standard"
                        name="name"
                        value={values.name}
                        error={Boolean(errors.name && touched.name)}
                        helperText={errors.name && touched.name && errors.name}
                        onChange={handleChange}
                        onBlur={handleBlur}
                      />
                      <Button type="submit">{strings.submit}</Button>
                    </Form>
                  );
                }}
              </Formik>
            </DialogContent>
          </>
        }
      />
    </>
  );
};

export default CreateTenantCard;
