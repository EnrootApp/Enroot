import { FormikConfig } from "formik";
import { RefObject } from "react";
import {
  GeneralSettingsForm,
  HandleDeleteImageProps,
  HandleFileChangeParams,
} from "../../../application/components/GeneralSettings/GeneralSettingsContainer.types";

export interface GeneralSettingsProps {
  formikConfig: FormikConfig<GeneralSettingsForm>;
  fileInputRef: RefObject<HTMLInputElement>;
  handleFileChange: ({
    event,
    setFieldValue,
    setFieldTouched,
  }: HandleFileChangeParams) => void;
  handleDeleteImage: ({
    avatarUrl,
    setFieldValue,
    setFieldTouched,
  }: HandleDeleteImageProps) => void;
  file: File | null;
  email: string;
}
