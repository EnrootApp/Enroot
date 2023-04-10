import { FormikConfig } from "formik";
import AddTasq from "../../../presentation/components/AddTasq/AddTasq";
import { AddTasqForm } from "./AddTasqContainer.types";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { useState } from "react";
import { useCreateTasqMutation } from "../../state/api/tasqApi";

const AddTasqContainer = () => {
  const [open, setOpen] = useState(false);
  const [createTasq] = useCreateTasqMutation();

  const validationSchema = Yup.object().shape({
    title: Yup.string().required(errorStrings.notEmpty).max(100),
    description: Yup.string().max(1000),
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

export default AddTasqContainer;
