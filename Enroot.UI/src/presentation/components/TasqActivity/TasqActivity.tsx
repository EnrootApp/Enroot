import { Box, Typography } from "@mui/material";
import { AccountModel } from "../../../domain/account/AccountModel";
import Bold from "../../uikit/Bold/Bold";
import strings from "../../localization/locales";
import { StatusEnum } from "../../../domain/tasq/StatusEnum";

interface Props {
  assignee: AccountModel;
  reviewer: AccountModel;
  message: string;
  status: StatusEnum;
  createdOn: string;
}

const TasqActivity: React.FC<Props> = ({
  assignee,
  reviewer,
  message,
  status,
  createdOn,
}) => {
  const statusMessages = {
    [StatusEnum.ToDo]: strings.todoMessage,
    [StatusEnum.InProgress]: strings.inProgressMessage,
    [StatusEnum.AwaitingReview]: strings.awaitingReviewMessage,
    [StatusEnum.Done]: strings.doneMessage,
    [StatusEnum.Rejected]: strings.rejectedMessage,
    [StatusEnum.Cancelled]: strings.rejectedMessage,
  };

  return (
    <Box sx={{ mt: 2, wordBreak: "break-word" }}>
      <Typography>{new Date(createdOn).toLocaleString()}</Typography>
      <Typography>
        <Bold>{reviewer.name}</Bold> {statusMessages[status]}
      </Typography>
      <Typography>{message}</Typography>
      <Typography>
        {strings.assignee}: <Bold>{assignee.name}</Bold>
      </Typography>
    </Box>
  );
};

export default TasqActivity;
