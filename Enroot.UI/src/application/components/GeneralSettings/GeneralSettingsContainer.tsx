import { FormikConfig } from "formik";
import { useEffect, useRef, useState } from "react";
import * as Yup from "yup";
import errorStrings from "../../../presentation/localization/errorMessages";
import {
  GeneralSettingsForm,
  HandleDeleteImageProps,
  HandleFileChangeParams,
} from "./GeneralSettingsContainer.types";
import GeneralSettings from "../../../presentation/components/GeneralSettings/GeneralSettings";
import { enqueueSnackbar } from "notistack";
import { useGetMeQuery, useUpdateInfoMutation } from "../../state/api/userApi";
import useS3FileUpload from "../../../infrastructure/storage/uploadToS3";
import apiStrings from "../../../presentation/localization/apiMessages";

const GeneralSettingsContainer: React.FC<{}> = () => {
  const { data, isLoading } = useGetMeQuery();
  const [updateInfo, { isSuccess }] = useUpdateInfoMutation();
  const [handleFileUpload, progress] = useS3FileUpload();

  const fileInputRef = useRef<HTMLInputElement>(null);
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = async ({
    event,
    setFieldValue,
    setFieldTouched,
  }: HandleFileChangeParams) => {
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

    handleFileUpload(uploadedFile).then((res) => {
      setFieldValue("avatarUrl", res.url);
      setFieldTouched("avatarUrl", true);
    });

    setFile(uploadedFile);
  };

  const handleDeleteImage = ({
    avatarUrl,
    setFieldValue,
    setFieldTouched,
  }: HandleDeleteImageProps) => {
    if (avatarUrl == "") {
      return;
    }

    setFile(null);

    setFieldValue("avatarUrl", "");
    setFieldTouched("avatarUrl", true);
  };

  useEffect(() => {
    if (isSuccess) {
      enqueueSnackbar(apiStrings.settingsUpdated, { variant: "success" });
    }
  }, [isSuccess]);

  const validationSchema = Yup.object().shape({
    firstName: Yup.string().required(errorStrings.notEmpty).max(50),
    lastName: Yup.string().required(errorStrings.notEmpty).max(50),
  });

  const formikConfig: FormikConfig<GeneralSettingsForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: {
      firstName: data?.firstName || "",
      lastName: data?.lastName || "",
      avatarUrl: data?.avatarUrl || "",
    },
    onSubmit: (values) => {
      updateInfo(values);
    },
  };

  return !isLoading ? (
    <GeneralSettings
      formikConfig={formikConfig}
      handleFileChange={handleFileChange}
      handleDeleteImage={handleDeleteImage}
      fileInputRef={fileInputRef}
      file={file}
      email={data?.email || ""}
      imageProgress={progress}
    />
  ) : null;
};

export default GeneralSettingsContainer;
