import { Box, DialogContent, DialogTitle } from "@mui/material";
import Dialog from "../../uikit/Dialog/Dialog";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import Button from "../../uikit/Button/Button";
import strings from "../../localization/locales";
import { StyledButton } from "./ConfirmationDialog.styles";

interface Props {
  onAgree: () => void;
  onDisagree: () => void;
  title: string;
  open: boolean;
  onClose: () => void;
}

const ConfirmationDialog: React.FC<Props> = ({
  onAgree,
  onDisagree,
  title,
  open,
  onClose,
}) => {
  return (
    <Dialog
      dialogProps={{
        fullWidth: true,
        maxWidth: "sm",
        open: open,
        onClose,
      }}
      dialogContent={
        <>
          <DialogTitle style={{ textAlign: "center" }}>
            <SubTitle value={title} />
          </DialogTitle>
          <DialogContent>
            <Box
              style={{
                display: "flex",
                gap: 16,
                flexWrap: "wrap",
              }}
            >
              <StyledButton
                onClick={() => {
                  onAgree();
                  onClose();
                }}
              >
                {strings.submit}
              </StyledButton>
              <StyledButton
                onClick={() => {
                  onDisagree();
                  onClose();
                }}
              >
                {strings.cancel}
              </StyledButton>
            </Box>
          </DialogContent>
        </>
      }
    />
  );
};

export default ConfirmationDialog;
