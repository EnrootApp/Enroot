import {
  Close,
  Done,
  EmojiObjects,
  NotInterested,
  Rule,
  Schedule,
} from "@mui/icons-material";
import { Status } from "../../../domain/tasq/Status";
import strings from "../../localization/locales";
import { TasqStatusContainer } from "./TasqStatus.styles";
import { Typography } from "@mui/material";

interface Props {
  value: Status;
}

const TasqStatus: React.FC<Props> = ({ value }) => {
  const iconsMap = {
    [Status.ToDo]: <Schedule />,
    [Status.InProgress]: <EmojiObjects />,
    [Status.AwaitingReview]: <Rule />,
    [Status.Done]: <Done />,
    [Status.Rejected]: <Close />,
    [Status.Cancelled]: <NotInterested />,
  };

  const textMap = {
    [Status.ToDo]: strings.toDo,
    [Status.InProgress]: strings.inProgress,
    [Status.AwaitingReview]: strings.awaitingReview,
    [Status.Done]: strings.done,
    [Status.Rejected]: strings.rejected,
    [Status.Cancelled]: strings.cancelled,
  };

  return (
    <TasqStatusContainer>
      {iconsMap[value]}
      <Typography>{textMap[value]}</Typography>
    </TasqStatusContainer>
  );
};

export default TasqStatus;
