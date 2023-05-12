import { enqueueSnackbar } from "notistack";
import { ChangeEventHandler, useRef } from "react";
import useS3FileUpload from "../../../infrastructure/storage/uploadToS3";
import errorStrings from "../../../presentation/localization/errorMessages";
import ImageUpload from "../../../presentation/components/FileUpload/FileUpload";

interface Props {
  imageSrc: string;
  setImageSrc: (imageSrc: string) => void;
  onUploadStart?: () => void;
  accept: string;
  radius: number;
}

const FileUploadContainer: React.FC<Props> = ({
  imageSrc,
  setImageSrc,
  onUploadStart,
  accept,
  radius,
}) => {
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

    if (fileSize > 100) {
      enqueueSnackbar(errorStrings.fileToBig, { variant: "error" });
      return;
    }

    onUploadStart && onUploadStart();
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
      accept={accept}
      radius={radius}
    />
  );
};

export default FileUploadContainer;
