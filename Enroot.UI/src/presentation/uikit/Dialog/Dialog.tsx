import * as React from "react";
import MuiDialog, { DialogProps } from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { TransitionProps } from "@mui/material/transitions";
import { Fade } from "@mui/material";

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>
) {
  return <Fade timeout={500} ref={ref} {...props} />;
});

interface Props {
  dialogProps: DialogProps;
  dialogContent: React.ReactNode;
}

const Dialog: React.FC<Props> = ({ dialogProps, dialogContent }) => {
  return (
    <MuiDialog TransitionComponent={Transition} {...dialogProps}>
      {dialogContent}
    </MuiDialog>
  );
};

export default Dialog;
