import { useSelector } from "react-redux";
import { Status } from "../../../domain/tasq/Status";
import { Tasq } from "../../../domain/tasq/Tasq";
import TasqToolbar from "../../../presentation/components/TasqToolbar/TasqToolbar";
import { Permission } from "../../common/enums/permission";
import { useGetMyAccountQuery } from "../../state/api/accountApi";
import {
  useApproveTasqMutation,
  useAssignTasqMutation,
  useCompleteTasqMutation,
  useRejectTasqMutation,
  useStartTasqMutation,
} from "../../state/api/tasqApi";
import { useState } from "react";
import { FormikConfig } from "formik";
import errorStrings from "../../../presentation/localization/errorMessages";
import * as Yup from "yup";

interface Props {
  tasq: Tasq;
}

const TasqToolbarContainer: React.FC<Props> = ({ tasq }) => {
  const [feedbackOpen, setFeedbackOpen] = useState(false);
  const { data: me, isLoading } = useGetMyAccountQuery({});
  const [start] = useStartTasqMutation();
  const [complete] = useCompleteTasqMutation();
  const [reject] = useRejectTasqMutation();
  const [approve] = useApproveTasqMutation();
  const [assign] = useAssignTasqMutation();

  const assignment = tasq.assignments[0];
  const tasqStatus = assignment?.status;

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

  const assignHandler = (assigneeId: string | undefined) => {
    if (!assigneeId) {
      return;
    }

    assign({ tasqId: tasq.id, assigneeId });
  };

  const canBeAssigned =
    !notEndedAssignment &&
    hasCreateTaskPermission &&
    tasqStatus !== Status.Done;

  const validationSchema = Yup.object().shape({
    feedbackMessage: Yup.string().required(errorStrings.notEmpty).max(255),
  });

  const formikConfig: FormikConfig<{ feedbackMessage: string }> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: { feedbackMessage: "" },
    onSubmit: async (values) => {
      reject({ id: assignment.id, feedbackMessage: values.feedbackMessage });
      setFeedbackOpen(false);
    },
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
      feedbackOpen={feedbackOpen}
      setFeedbackOpen={setFeedbackOpen}
      formikConfig={formikConfig}
      canBeAssigned={canBeAssigned}
      assignHandler={assignHandler}
    />
  );
};

export default TasqToolbarContainer;
