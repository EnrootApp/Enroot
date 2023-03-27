import { FormikConfig } from "formik";
import { ChangeEvent, useRef, useState } from "react";
import strings from "../../../presentation/localization/locales";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import { GeneralSettingsForm } from "./GeneralSettingsContainer.types";
import GeneralSettings from "../../../presentation/components/GeneralSettings/GeneralSettings";
import { enqueueSnackbar } from "notistack";

errorStrings.setLanguage("ru");
strings.setLanguage("ru");

const validationSchema = Yup.object().shape({
  firstName: Yup.string().required(errorStrings.notEmpty).max(50),
  lastName: Yup.string().required(errorStrings.notEmpty).max(50),
});

const GeneralSettingsContainer: React.FC<{}> = () => {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [file, setFile] = useState<File | null>(null);
  const [imageUrl, setImageUrl] = useState<string | null>(null);

  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    event.preventDefault();
    const uploadedFile = event.target.files?.[0];
    if (!uploadedFile) {
      return;
    }

    const fileSize = uploadedFile.size / 1024 / 1024;

    if (fileSize > 10) {
      enqueueSnackbar(errorStrings.fileToBig, { variant: "error" });
      return;
    }

    setFile(uploadedFile);
    const url = URL.createObjectURL(uploadedFile);
    setImageUrl(url);
  };

  const handleDeleteImage = () => {
    setFile(null);
    setImageUrl(null);
  };

  const formikConfig: FormikConfig<GeneralSettingsForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { firstName: "", lastName: "", avatarUrl: "" },
    onSubmit: (values) => {
      console.log(values);
    },
  };

  return (
    <GeneralSettings
      formikConfig={formikConfig}
      handleFileChange={handleFileChange}
      handleDeleteImage={handleDeleteImage}
      fileInputRef={fileInputRef}
      imageUrl={imageUrl}
      file={file}
    />
  );
};

export default GeneralSettingsContainer;
