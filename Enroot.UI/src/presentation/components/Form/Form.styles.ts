import { styled } from "@mui/material/styles";
import { Form as FormikForm, FormikFormProps } from "formik";

export const StyledForm = styled(FormikForm)<FormikFormProps>(({ theme }) => ({
  display: "flex",
  flexDirection: "column",
  gap: 20,
}));
