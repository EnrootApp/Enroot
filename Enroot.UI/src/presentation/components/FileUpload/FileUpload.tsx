import { Cancel, Edit } from "@mui/icons-material";
import { Avatar } from "@mui/material";
import { ChangeEventHandler, RefObject } from "react";
import CircularProgressCentered from "../../uikit/CircularProgressCentered/CircularProgressCentered";
import { AvatarDiv, ButtonDiv, StyledIconButton } from "./FileUpload.styles";

interface Props {
  fileInputRef: RefObject<HTMLInputElement>;
  handleFileChange: ChangeEventHandler<HTMLInputElement>;
  handleDeleteImage: () => void;
  progress: number;
  imageSrc: string;
  accept: string;
  radius: number;
}

const FileUpload: React.FC<Props> = ({
  fileInputRef,
  handleFileChange,
  handleDeleteImage,
  progress,
  imageSrc,
  accept,
  radius,
}) => {
  const isInProgress = progress !== 0 && progress !== 100;

  return (
    <div style={{ display: "flex" }}>
      <input
        accept={accept}
        type="file"
        hidden
        ref={fileInputRef}
        onChange={handleFileChange}
      />
      <AvatarDiv>
        <Avatar
          src={imageSrc || "/imagePlaceholder.jpg"}
          sx={{
            width: radius,
            height: radius,
            opacity: isInProgress ? 0.4 : 1,
            transition: "0.2s",
          }}
          className="avatar"
        />
        {isInProgress && (
          <CircularProgressCentered
            variant="determinate"
            color="secondary"
            value={progress}
          />
        )}
        <ButtonDiv>
          {imageSrc ? (
            <StyledIconButton
              className="icon"
              onClick={(event) => handleDeleteImage()}
            >
              <Cancel />
            </StyledIconButton>
          ) : (
            <StyledIconButton
              className="icon"
              onClick={() => fileInputRef.current?.click()}
            >
              <Edit />
            </StyledIconButton>
          )}
        </ButtonDiv>
      </AvatarDiv>
    </div>
  );
};

export default FileUpload;
