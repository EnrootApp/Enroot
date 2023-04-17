import {
  Box,
  DialogContent,
  DialogTitle,
  Grid,
  Typography,
} from "@mui/material";
import SubTitle from "../../uikit/SubTitle/SubTitle";
import strings from "../../localization/locales";
import User from "../../uikit/User/User";
import TasqStatus from "../../uikit/Status/TasqStatus";
import { Status } from "../../../domain/tasq/Status";
import { Tasq } from "../../../domain/tasq/Tasq";
import Button from "../../uikit/Button/Button";
import Dialog from "../../uikit/Dialog/Dialog";
import { Formik, FormikConfig, FormikProps } from "formik";
import Form from "../../uikit/Form/Form";
import Input from "../../uikit/Input/Input";
import SelectAccountContainer from "../../../application/components/SelectAccount/SelectAccountContainer";

interface Props {
  tasq: Tasq;
  showStartButton: boolean;
  showCancelButton: boolean;
  showSendForReviewButton: boolean;
  showReviewButtons: boolean;
  onStartButtonClick: () => void;
  onCompleteButtonClick: () => void;
  onApproveButtonClick: () => void;
  setFeedbackOpen: (value: boolean) => void;
  feedbackOpen: boolean;
  formikConfig: FormikConfig<{ feedbackMessage: string }>;
  canBeAssigned: boolean;
  assignHandler: (value: string | undefined) => void;
}

const TasqToolbar: React.FC<Props> = ({
  tasq,
  showStartButton,
  showSendForReviewButton,
  showReviewButtons,
  onStartButtonClick,
  onCompleteButtonClick,
  onApproveButtonClick,
  setFeedbackOpen,
  feedbackOpen,
  formikConfig,
  canBeAssigned,
  assignHandler,
}) => {
  return (
    <Box style={{ flex: 0.7 }}>
      <SubTitle value={strings.information} />
      <Grid container spacing={3} columns={16}>
        <Grid item xs={6}>
          <Typography>{strings.status}</Typography>
        </Grid>
        <Grid item xs={10}>
          <TasqStatus value={tasq.assignments[0]?.status || Status.ToDo} />
        </Grid>
        <Grid item xs={6}>
          <Typography>{strings.creator}</Typography>
        </Grid>
        <Grid item xs={10}>
          <User imageSrc={tasq.creator.avatarUrl} name={tasq.creator.name} />
        </Grid>
        <Grid item xs={6}>
          <Typography>{strings.assignee}</Typography>
        </Grid>
        <Grid item xs={10}>
          {canBeAssigned ? (
            <div title="Нажмите, чтобы назначить">
              <SelectAccountContainer onChange={assignHandler} />
            </div>
          ) : (
            <User
              imageSrc={tasq.assignments[0]?.assignee.avatarUrl || ""}
              name={tasq.assignments[0]?.assignee.name || strings.emptyName}
            />
          )}
        </Grid>
        <Grid item xs={6}>
          <Typography>{strings.created}</Typography>
        </Grid>
        <Grid item xs={10}>
          <Typography>{new Date(tasq.createdOn).toLocaleString()}</Typography>
        </Grid>
      </Grid>

      <Box sx={{ mt: 3, display: "flex", gap: 2 }}>
        {showStartButton && (
          <Button sx={{ flex: 1 }} onClick={() => onStartButtonClick()}>
            {strings.start}
          </Button>
        )}
        {showSendForReviewButton && (
          <Button sx={{ flex: 1 }} onClick={() => onCompleteButtonClick()}>
            {strings.complete}
          </Button>
        )}
        {showReviewButtons && (
          <>
            <Button sx={{ flex: 1 }} onClick={() => onApproveButtonClick()}>
              {strings.approve}
            </Button>
            <Button sx={{ flex: 1 }} onClick={() => setFeedbackOpen(true)}>
              {strings.reject}
            </Button>
          </>
        )}
      </Box>

      <Dialog
        dialogProps={{
          fullWidth: true,
          maxWidth: "md",
          open: feedbackOpen,
          onClose: () => setFeedbackOpen(false),
        }}
        dialogContent={
          <>
            <DialogTitle>
              <SubTitle value={strings.feedback} />
            </DialogTitle>
            <DialogContent>
              <Formik {...formikConfig}>
                {(props: FormikProps<{ feedbackMessage: string }>) => {
                  const { values, touched, errors, handleBlur, handleChange } =
                    props;
                  return (
                    <Form noValidate>
                      <Input
                        label={strings.feedback}
                        variant="standard"
                        name="feedbackMessage"
                        value={values.feedbackMessage}
                        error={Boolean(
                          errors.feedbackMessage && touched.feedbackMessage
                        )}
                        helperText={
                          errors.feedbackMessage &&
                          touched.feedbackMessage &&
                          errors.feedbackMessage
                        }
                        onChange={handleChange}
                        onBlur={handleBlur}
                      />
                      <Button type="submit">{strings.submit}</Button>
                    </Form>
                  );
                }}
              </Formik>
            </DialogContent>
          </>
        }
      />
    </Box>
  );
};

export default TasqToolbar;
