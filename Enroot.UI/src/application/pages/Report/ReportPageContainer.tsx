import * as Yup from "yup";
import ReportPage from "../../../presentation/pages/Report/ReportPage";
import { useLazyGetReportQuery } from "../../state/api/tasqApi";
import { FormikConfig } from "formik";
import { GetReportForm } from "./ReportPageContainer.types";
import errorStrings from "../../../presentation/localization/errorMessages";
import dayjs from "dayjs";

const ReportPageContainer: React.FC<{}> = () => {
  const [getReport, report] = useLazyGetReportQuery();

  const day = 24 * 60 * 60 * 1000;

  const validationSchema = Yup.object().shape({
    from: Yup.date().required(errorStrings.notEmpty),
    to: Yup.date()
      .required(errorStrings.notEmpty)
      .min(Yup.ref("from"), errorStrings.dateAfter)
      .test(
        "",
        errorStrings.formatString(errorStrings.dateRange, "30").toString(),
        function (value) {
          const { from } = this.parent;
          const maxDate = new Date(from.getTime() + 30 * day);
          return value <= maxDate;
        }
      ),
  });

  const formikConfig: FormikConfig<GetReportForm> = {
    validationSchema: validationSchema,
    validateOnBlur: true,
    validateOnMount: true,
    initialValues: {
      from: dayjs().subtract(10, "day"),
      to: dayjs(),
    },
    onSubmit: async ({ from, to }) => {
      await getReport({
        from: from.format("MM/DD/YYYY"),
        to: to.format("MM/DD/YYYY"),
      });
    },
  };

  return (
    <ReportPage
      formikConfig={formikConfig}
      report={report.data}
      isLoading={report.isLoading}
      fetched={!report.isUninitialized}
    />
  );
};

export default ReportPageContainer;
