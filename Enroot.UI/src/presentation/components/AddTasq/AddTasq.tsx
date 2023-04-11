import { boolean } from "yup";
import strings from "../../localization/locales";
import Button from "../../uikit/Button/Button";
import Dialog from "../../uikit/Dialog/Dialog";
import { DialogContent, DialogTitle, Typography } from "@mui/material";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import { Formik, FormikConfig, FormikProps } from "formik";
import Form from "../../uikit/Form/Form";
import { AddTasqForm } from "../../../application/components/AddTasq/AddTasqContainer.types";
import SelectAccountContainer from "../../../application/components/SelectAccount/SelectAccountContainer";
import Input from "../../uikit/Input/Input";

interface Props {
  open: boolean;
  formikConfig: FormikConfig<AddTasqForm>;
  setOpen: (value: boolean) => void;
}

const AddTasq: React.FC<Props> = ({ open, formikConfig, setOpen }) => {
  return (
    <>
      <Button onClick={() => setOpen(true)}>{strings.addTasq}</Button>
      <Dialog
        dialogProps={{
          fullWidth: true,
          maxWidth: "md",
          open: open,
          onClose: () => setOpen(false),
        }}
        dialogContent={
          <>
            <DialogTitle>
              <SubTitle value={strings.addTasq} />
            </DialogTitle>
            <DialogContent>
              <Formik {...formikConfig}>
                {(props: FormikProps<AddTasqForm>) => {
                  const {
                    values,
                    touched,
                    errors,
                    handleBlur,
                    handleChange,
                    setFieldTouched,
                    setFieldValue,
                  } = props;
                  return (
                    <Form noValidate>
                      <Input
                        label={strings.summary}
                        variant="standard"
                        name="title"
                        value={values.title}
                        error={Boolean(errors.title && touched.title)}
                        helperText={
                          errors.title && touched.title && errors.title
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                      />
                      <Input
                        label={strings.description}
                        variant="standard"
                        name="description"
                        multiline
                        value={values.description}
                        error={Boolean(
                          errors.description && touched.description
                        )}
                        helperText={
                          errors.description &&
                          touched.description &&
                          errors.description
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                      />
                      <input
                        name="assigneeId"
                        hidden
                        value={values.assigneeId}
                      />
                      <Typography>Исполнитель: </Typography>
                      <SelectAccountContainer
                        onChange={(value) => {
                          setFieldTouched("assigneeId", true);
                          setFieldValue("assigneeId", value);
                        }}
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

export default AddTasq;
