import { Cancel, Edit } from "@mui/icons-material";
import { Avatar } from "@mui/material";
import { ChangeEventHandler, MouseEventHandler, RefObject } from "react";
import CircularProgressCentered from "../CircularProgressCentered/CircularProgressCentered";
import { AvatarDiv, ButtonDiv, StyledIconButton } from "./ImageUpload.styles";

interface Props {
  fileInputRef: RefObject<HTMLInputElement>;
  handleFileChange: ChangeEventHandler<HTMLInputElement>;
  handleDeleteImage: MouseEventHandler<HTMLButtonElement>;
  progress: number;
  imageSrc: string;
}

const ImageUpload: React.FC<Props> = ({
  fileInputRef,
  handleFileChange,
  handleDeleteImage,
  progress,
  imageSrc,
}) => {
  const isInProgress = progress !== 0 && progress !== 100;

  return (
    <div style={{ display: "flex" }}>
      <input
        accept="image/*"
        type="file"
        hidden
        ref={fileInputRef}
        onChange={handleFileChange}
      />
      <AvatarDiv>
        <Avatar
          src={imageSrc}
          sx={{
            width: 140,
            height: 140,
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
            <StyledIconButton className="icon" onClick={handleDeleteImage}>
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

export default ImageUpload;
