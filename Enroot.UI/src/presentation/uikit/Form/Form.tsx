import { FormikFormProps } from "formik";
import React from "react";
import { StyledForm } from "./Form.styles";

const Form: React.FC<FormikFormProps> = (props) => {
  return <StyledForm {...props} />;
};

export default Form;
