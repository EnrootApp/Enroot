import { FormikConfig } from "formik";
import { InviteAccountForm } from "./InviteAccountContainer.types";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { useState } from "react";
import { useInviteAccountMutation } from "../../state/api/accountApi";
import { Role } from "../../common/enums/role";
import InviteAccount from "../../../presentation/components/InviteAccount/InviteAccount";

const InviteAccountContainer = () => {
  const [open, setOpen] = useState(false);
  const [invite] = useInviteAccountMutation();

  const validationSchema = Yup.object().shape({
    email: Yup.string()
      .matches(
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        errorStrings.invalidEmail
      )
      .required(errorStrings.notEmpty),
    description: Yup.string().max(1000),
  });

  const formikConfig: FormikConfig<InviteAccountForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { email: "", role: Role.Default },
    onSubmit: async (values) => {
      invite(values);
      setOpen(false);
    },
  };

  return (
    <InviteAccount open={open} setOpen={setOpen} formikConfig={formikConfig} />
  );
};

export default InviteAccountContainer;
