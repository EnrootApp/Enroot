import { useEffect } from "react";
import FileUploader from "../../../presentation/components/FileUploader/FileUploader";
import { useDispatch, useSelector } from "react-redux";
import { addAttachment, reset } from "../../state/components/fileUploaderSlice";

const FileUploaderContainer = () => {
  const dispatch = useDispatch();

  const uploadedFiles = useSelector(
    (state) => state.components.fileUploaderSlice
  );

  const setImageSrc = (url: string) => {
    const baseUrl =
      "https://s3.eu-central-003.backblazeb2.com/EnrootAttachments/";
    const result = url.substring(baseUrl.length);

    dispatch(addAttachment({ name: result, url }));
  };

  useEffect(() => {
    return () => dispatch(reset());
  }, []);

  return (
    <FileUploader uploadedFiles={uploadedFiles} setImageSrc={setImageSrc} />
  );
};

export default FileUploaderContainer;
