import { enqueueSnackbar } from "notistack";
import { ChangeEventHandler, useRef, useState } from "react";
import useS3FileUpload from "../../../infrastructure/storage/uploadToS3";
import errorStrings from "../../../presentation/localization/errorMessages";
import ImageUpload from "../../../presentation/uikit/ImageUpload/ImageUpload";

interface Props {
  imageSrc: string;
  setImageSrc: (imageSrc: string) => void;
}

const ImageUploadContainer: React.FC<Props> = ({ imageSrc, setImageSrc }) => {
  const [handleFileUpload, progress] = useS3FileUpload();

  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFileChange: ChangeEventHandler<HTMLInputElement> = async (
    event
  ) => {
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

    var result = await handleFileUpload(uploadedFile);
    setImageSrc(result.url);
  };

  const handleDeleteImage = () => {
    setImageSrc("");
  };

  return (
    <ImageUpload
      fileInputRef={fileInputRef}
      handleDeleteImage={handleDeleteImage}
      handleFileChange={handleFileChange}
      progress={progress}
      imageSrc={imageSrc}
    />
  );
};

export default ImageUploadContainer;
