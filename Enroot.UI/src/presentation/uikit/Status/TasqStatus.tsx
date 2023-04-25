import {
  Close,
  Done,
  EmojiObjects,
  NotInterested,
  Rule,
  Schedule,
} from "@mui/icons-material";
import { StatusEnum } from "../../../domain/tasq/StatusEnum";
import strings from "../../localization/locales";
import { TasqStatusContainer } from "./TasqStatus.styles";
import { Typography } from "@mui/material";

interface Props {
  value: StatusEnum;
}

const TasqStatus: React.FC<Props> = ({ value }) => {
  const iconsMap = {
    [StatusEnum.ToDo]: <Schedule />,
    [StatusEnum.InProgress]: <EmojiObjects />,
    [StatusEnum.AwaitingReview]: <Rule />,
    [StatusEnum.Done]: <Done />,
    [StatusEnum.Rejected]: <Close />,
    [StatusEnum.Cancelled]: <NotInterested />,
  };

  const textMap = {
    [StatusEnum.ToDo]: strings.toDo,
    [StatusEnum.InProgress]: strings.inProgress,
    [StatusEnum.AwaitingReview]: strings.awaitingReview,
    [StatusEnum.Done]: strings.done,
    [StatusEnum.Rejected]: strings.rejected,
    [StatusEnum.Cancelled]: strings.cancelled,
  };

  return (
    <TasqStatusContainer>
      {iconsMap[value]}
      <Typography>{textMap[value]}</Typography>
    </TasqStatusContainer>
  );
};

export default TasqStatus;
