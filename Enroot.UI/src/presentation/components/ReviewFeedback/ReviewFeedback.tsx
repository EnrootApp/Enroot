import { Box, Typography } from "@mui/material";
import { AccountModel } from "../../../domain/account/AccountModel";
import Bold from "../../uikit/Bold/Bold";
import strings from "../../localization/locales";

interface Props {
  assignee: AccountModel;
  reviewer: AccountModel;
  message: string;
}

const ReviewFeedback: React.FC<Props> = ({ assignee, reviewer, message }) => {
  return (
    <Box sx={{ mt: 2, wordBreak: "break-word" }}>
      <Typography>
        <Bold>{reviewer.name}</Bold> {strings.rejectedMessage}
      </Typography>
      <Typography>{message}</Typography>
      <Typography>
        {strings.assignee}: <Bold>{assignee.name}</Bold>
      </Typography>
    </Box>
  );
};

export default ReviewFeedback;
