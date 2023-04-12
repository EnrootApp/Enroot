import strings from "../../localization/locales";
import Button from "../../uikit/Button/Button";
import Dialog from "../../uikit/Dialog/Dialog";
import {
  DialogContent,
  DialogTitle,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@mui/material";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import { Formik, FormikConfig, FormikProps } from "formik";
import Form from "../../uikit/Form/Form";
import Input from "../../uikit/Input/Input";
import { InviteAccountForm } from "../../../application/components/InviteAccount/InviteAccountContainer.types";
import { Role } from "../../../application/common/enums/role";

interface Props {
  open: boolean;
  formikConfig: FormikConfig<InviteAccountForm>;
  setOpen: (value: boolean) => void;
}

const InviteAccount: React.FC<Props> = ({ open, formikConfig, setOpen }) => {
  return (
    <>
      <Button onClick={() => setOpen(true)}>{strings.invite}</Button>
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
              <SubTitle value={strings.invite} />
            </DialogTitle>
            <DialogContent>
              <Formik {...formikConfig}>
                {(props: FormikProps<InviteAccountForm>) => {
                  const { values, touched, errors, handleBlur, handleChange } =
                    props;
                  return (
                    <Form noValidate>
                      <Input
                        label={strings.email}
                        variant="standard"
                        name="email"
                        value={values.email}
                        error={Boolean(errors.email && touched.email)}
                        helperText={
                          errors.email && touched.email && errors.email
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                      />
                      <FormControl variant="standard">
                        <InputLabel id="demo-simple-select-standard-label">
                          {strings.role}
                        </InputLabel>
                        <Select
                          name="roleId"
                          value={values.roleId}
                          onChange={handleChange}
                          onBlur={handleBlur}
                          error={Boolean(errors.roleId && touched.roleId)}
                        >
                          <MenuItem value={Role.Default}>
                            {strings.defaultRole}
                          </MenuItem>
                          <MenuItem value={Role.Moderator}>
                            {strings.moderatorRole}
                          </MenuItem>
                          <MenuItem value={Role.TenantAdmin}>
                            {strings.tenantAdminRole}
                          </MenuItem>
                        </Select>
                      </FormControl>
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

export default InviteAccount;
