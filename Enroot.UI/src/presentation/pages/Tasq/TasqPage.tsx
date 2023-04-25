import { Box, Container } from "@mui/material";
import { Tasq } from "../../../domain/tasq/Tasq";
import TenantTitle from "../../uikit/TenantTitle/TenantTitle";
import Title from "../../uikit/Title/Title";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import strings from "../../localization/locales";
import InlineEditContainer from "../../../application/components/InlineEdit/InlineEditContainer";
import TasqToolbarContainer from "../../../application/components/TasqToolbar/TasqToolbarContainer";
import FileUploaderContainer from "../../../application/components/FileUploader/FileUploaderContainer";
import { StatusEnum } from "../../../domain/tasq/StatusEnum";
import TasqActivity from "../../components/TasqActivity/TasqActivity";

interface Props {
  tasq: Tasq;
  updateTasq: (tasq: { description: string; id: string }) => void;
  hasPermissionToChange: boolean;
}

const TasqPage: React.FC<Props> = ({
  tasq,
  updateTasq,
  hasPermissionToChange,
}) => {
  const currentAssignment = tasq.assignments[0];
  const currentAssignmentStatus = currentAssignment?.statuses[0].status;

  const isEditingDisabled = !(
    hasPermissionToChange &&
    (!currentAssignment ||
      currentAssignmentStatus === StatusEnum.ToDo ||
      currentAssignmentStatus === StatusEnum.Rejected)
  );

  return (
    <Box style={{ width: "100%" }}>
      <TenantTitle title={tasq.title} />
      <Container
        style={{
          display: "flex",
          flexDirection: "column",
        }}
      >
        <Box>
          <Title value={`${strings.summary}: ${tasq.title}`}></Title>
        </Box>

        <Box style={{ display: "flex", flexWrap: "wrap", gap: 48 }}>
          <Box style={{ flex: 1, minWidth: 230 }}>
            <SubTitle value={strings.description}></SubTitle>
            <InlineEditContainer
              placeholder={strings.noDescription}
              text={tasq.description}
              multiline
              onEditEnd={(value) =>
                updateTasq({ description: value || "", id: tasq.id })
              }
              disabled={isEditingDisabled}
            />
            {currentAssignmentStatus === StatusEnum.InProgress && (
              <>
                <SubTitle value={strings.attachments}></SubTitle>
                <FileUploaderContainer />
              </>
            )}
            {currentAssignmentStatus === StatusEnum.AwaitingReview && (
              <>
                <SubTitle value={strings.attachments}></SubTitle>
                <Box style={{ display: "flex", flexWrap: "wrap", gap: 16 }}>
                  {currentAssignment?.attachments.map((attachment) => (
                    <a
                      href={attachment.url}
                      target="_blank"
                      key={attachment.url}
                    >
                      <img
                        src={attachment.url}
                        style={{
                          height: 150,
                          width: 150,
                          objectFit: "contain",
                          borderRadius: "50%",
                        }}
                      />
                    </a>
                  ))}
                </Box>
              </>
            )}

            <SubTitle value={strings.activity} />
            {tasq.assignments.map((a) =>
              a.statuses.map((s) => (
                <TasqActivity
                  key={s.createdOn}
                  assignee={a.assignee}
                  reviewer={s.approver}
                  message={s.feedbackMessage}
                  createdOn={s.createdOn}
                  status={s.status}
                />
              ))
            )}
          </Box>

          <TasqToolbarContainer tasq={tasq} />
        </Box>
      </Container>
    </Box>
  );
};

export default TasqPage;
