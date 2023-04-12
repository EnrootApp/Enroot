import { Box, FormControl, FormHelperText, Typography } from "@mui/material";
import strings from "../../../presentation/localization/locales";
import TenantTitle from "../../../presentation/uikit/TenantTitle/TenantTitle";
import { Formik, FormikConfig, FormikProps } from "formik";
import { GetReportForm } from "../../../application/pages/Report/ReportPageContainer.types";
import Form from "../../uikit/Form/Form";
import Button from "../../uikit/Button/Button";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { Report } from "../../../domain/tasq/Report";
import CircularProgressCentered from "../../uikit/CircularProgressCentered/CircularProgressCentered";

interface Props {
  formikConfig: FormikConfig<GetReportForm>;
  report: Report | undefined;
  isLoading: boolean;
  fetched: boolean;
}

const ReportPage: React.FC<Props> = ({
  formikConfig,
  report,
  isLoading,
  fetched,
}) => {
  return isLoading ? (
    <CircularProgressCentered />
  ) : (
    <Box style={{ width: "100%" }}>
      <Box style={{ margin: 4 }}>
        <TenantTitle title={strings.reports} />
      </Box>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <Formik {...formikConfig}>
          {(props: FormikProps<GetReportForm>) => {
            const { values, touched, errors, setFieldValue } = props;
            return (
              <Form noValidate>
                <div
                  style={{
                    padding: "0 20px",
                    display: "flex",
                    justifyContent: "space-between",
                    flexWrap: "wrap",
                    gap: 20,
                  }}
                >
                  <FormControl sx={{ flex: 1, minWidth: 200 }}>
                    <DatePicker
                      value={values.from}
                      label={strings.from}
                      onChange={(value) => setFieldValue("from", value)}
                    />
                    <FormHelperText error={Boolean(errors.from)}>
                      {errors.from}
                    </FormHelperText>
                  </FormControl>

                  <FormControl sx={{ flex: 1, minWidth: 200 }}>
                    <DatePicker
                      value={values.to}
                      label={strings.to}
                      onChange={(value) => setFieldValue("to", value)}
                    />
                    <FormHelperText error={Boolean(errors.from)}>
                      {errors.to}
                    </FormHelperText>
                  </FormControl>

                  <Button
                    type="submit"
                    sx={{ flex: 1, minWidth: 200, height: 56 }}
                  >
                    {strings.submit}
                  </Button>
                </div>
              </Form>
            );
          }}
        </Formik>
      </LocalizationProvider>
      {fetched && (
        <Box style={{ margin: 20 }}>
          <Typography>{`${strings.reportTotal}: ${report?.totalAmount}`}</Typography>
          <Typography>{`${strings.reportDone}: ${report?.doneAmount}`}</Typography>
          <Typography>{`${strings.reportRejected}: ${report?.rejectedAmount}`}</Typography>
          <Typography>{`${strings.reportAwaiting}: ${report?.awaitingReviewAmount}`}</Typography>
          <Typography>{`${strings.reportInProgress}: ${report?.inProgressAmount}`}</Typography>
          <Typography>{`${strings.reportToDo}: ${report?.todoAmount}`}</Typography>
          <Typography>{`${strings.reportNotAssigned}: ${report?.notAssignedAmount}`}</Typography>
        </Box>
      )}
    </Box>
  );
};

export default ReportPage;
