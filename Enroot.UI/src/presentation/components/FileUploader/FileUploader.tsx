import { Box } from "@mui/material";
import { Attachment } from "../../../domain/tasq/Attachment";
import ImageUploadContainer from "../../../application/components/ImageUpload/ImageUploadContainer";

interface Props {
  uploadedFiles: Attachment[];
  setImageSrc: (url: string) => void;
}

const FileUploader: React.FC<Props> = ({ uploadedFiles, setImageSrc }) => {
  return (
    <Box style={{ display: "flex", flexWrap: "wrap", gap: 16 }}>
      {uploadedFiles.map((attachment) => (
        <img
          key={attachment.url}
          src={attachment.url}
          style={{
            height: 90,
            width: 90,
            objectFit: "contain",
            borderRadius: "50%",
          }}
        />
      ))}
      <Box style={{ height: 90, width: 90 }}>
        <ImageUploadContainer
          setImageSrc={(url) => setImageSrc(url)}
          imageSrc=""
        />
      </Box>
    </Box>
  );
};

export default FileUploader;
