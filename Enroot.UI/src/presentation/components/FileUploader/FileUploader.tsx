import { Box } from "@mui/material";
import { Attachment } from "../../../domain/tasq/Attachment";
import ImageUploadContainer from "../../../application/components/FileUpload/FileUploadContainer";
import { Image, StyledBox } from "./FileUploader.styles";

interface Props {
  uploadedFiles: Attachment[];
  setImageSrc: (url: string) => void;
}

const FileUploader: React.FC<Props> = ({ uploadedFiles, setImageSrc }) => {
  return (
    <StyledBox>
      {uploadedFiles.map((attachment) => (
        <Image key={attachment.url} src={attachment.url} />
      ))}
      <Box style={{ height: 90, width: 90 }}>
        <ImageUploadContainer
          setImageSrc={(url) => setImageSrc(url)}
          imageSrc=""
          radius={150}
          accept="file/*"
        />
      </Box>
    </StyledBox>
  );
};

export default FileUploader;
