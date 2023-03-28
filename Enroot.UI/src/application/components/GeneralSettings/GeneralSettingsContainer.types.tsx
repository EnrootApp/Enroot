import { ChangeEvent } from "react";

export interface GeneralSettingsForm {
  firstName: string;
  lastName: string;
  avatarUrl: string;
}

export interface HandleFileChangeParams {
  event: ChangeEvent<HTMLInputElement>;
  setFieldValue: (
    field: string,
    value: any,
    shouldValidate?: boolean | undefined
  ) => void;
  setFieldTouched: (
    field: string,
    isTouched?: boolean | undefined,
    shouldValidate?: boolean | undefined
  ) => void;
}

export interface HandleDeleteImageProps {
  setFieldValue: (
    field: string,
    value: any,
    shouldValidate?: boolean | undefined
  ) => void;
  setFieldTouched: (
    field: string,
    isTouched?: boolean | undefined,
    shouldValidate?: boolean | undefined
  ) => void;
  avatarUrl: string;
}
