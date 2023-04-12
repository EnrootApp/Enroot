import { FormikConfig } from "formik";
import AddTasq from "../../../presentation/components/AddTasq/AddTasq";
import { AddTasqForm } from "./InviteUser.types";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { useState } from "react";
import { useCreateTasqMutation } from "../../state/api/tasqApi";

const InviteUser = () => {
  const [open, setOpen] = useState(false);
  const [createTasq] = useCreateTasqMutation();

  const validationSchema = Yup.object().shape({
    email: Yup.string()
      .matches(
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        errorStrings.invalidEmail
      )
      .required(errorStrings.notEmpty),
  });

  const formikConfig: FormikConfig<AddTasqForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { title: "", description: "" },
    onSubmit: async (values) => {
      createTasq(values);
      setOpen(false);
    },
  };

  return <AddTasq open={open} setOpen={setOpen} formikConfig={formikConfig} />;
};

export default InviteUser;
