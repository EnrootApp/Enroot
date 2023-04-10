import { useSelector } from "react-redux";
import { Status } from "../../../domain/tasq/Status";
import { Tasq } from "../../../domain/tasq/Tasq";
import TasqToolbar from "../../../presentation/components/TasqToolbar/TasqToolbar";
import { Permission } from "../../common/enums/permission";
import { useGetMyAccountQuery } from "../../state/api/accountApi";
import {
  useApproveTasqMutation,
  useCompleteTasqMutation,
  useRejectTasqMutation,
  useStartTasqMutation,
} from "../../state/api/tasqApi";

interface Props {
  tasq: Tasq;
}

const TasqToolbarContainer: React.FC<Props> = ({ tasq }) => {
  const { data: me, isLoading } = useGetMyAccountQuery({});
  const [start] = useStartTasqMutation();
  const [complete] = useCompleteTasqMutation();
  const [reject] = useRejectTasqMutation();
  const [approve] = useApproveTasqMutation();

  const assignment = tasq.assignments[0];
  const tasqStatus = assignment?.status || Status.ToDo;

  const notEndedAssignment =
    tasqStatus === Status.ToDo ||
    tasqStatus === Status.InProgress ||
    tasqStatus === Status.AwaitingReview;

  const hasCreateTaskPermission =
    me?.permissions.includes(Permission.CreateTasq) || false;

  const hasReviewTaskPermission =
    me?.permissions.includes(Permission.ReviewTasq) || false;

  const amIAssignee = assignment && assignment.assignee.id === me?.id;

  const showStartButton =
    assignment && amIAssignee && tasqStatus === Status.ToDo;

  const showSendForReviewButton =
    tasqStatus === Status.InProgress && amIAssignee;

  const showCancelButton =
    false && assignment && notEndedAssignment && hasCreateTaskPermission;

  const showReviewButtons =
    tasqStatus === Status.AwaitingReview && hasReviewTaskPermission;

  const startAssignmentHandler = () => {
    start({ id: assignment.id });
  };

  const attachments = useSelector(
    (state) => state.components.fileUploaderSlice
  );

  const sendForReviewHandler = () => {
    complete({ id: assignment.id, attachments });
  };

  const approveHandler = () => {
    approve({ id: assignment.id });
  };

  const rejectHandler = () => {
    reject({ id: assignment.id, feedbackMessage: "" });
  };

  return isLoading ? null : (
    <TasqToolbar
      tasq={tasq}
      showStartButton={showStartButton}
      showCancelButton={showCancelButton}
      showSendForReviewButton={showSendForReviewButton}
      showReviewButtons={showReviewButtons}
      onStartButtonClick={startAssignmentHandler}
      onCompleteButtonClick={sendForReviewHandler}
      onApproveButtonClick={approveHandler}
      onRejectButtonClick={rejectHandler}
    />
  );
};

export default TasqToolbarContainer;
