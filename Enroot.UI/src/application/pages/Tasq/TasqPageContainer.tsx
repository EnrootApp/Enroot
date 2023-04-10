import TasqPage from "../../../presentation/pages/Tasq/TasqPage";
import CircularProgressCentered from "../../../presentation/uikit/CircularProgressCentered/CircularProgressCentered";
import { Permission } from "../../common/enums/permission";
import { useGetMyAccountQuery } from "../../state/api/accountApi";
import {
  useGetTasqQuery,
  useUpdateTasqMutation,
} from "../../state/api/tasqApi";
import { useParams } from "react-router-dom";

const TasqPageContainer = () => {
  const { id } = useParams();

  const { data, isFetching: tasqsFetching } = useGetTasqQuery({ id: id! });
  const { data: me, isFetching: permissionsFetching } = useGetMyAccountQuery(
    {}
  );
  const [updateTasq, { isSuccess }] = useUpdateTasqMutation();

  const hasPermissionToChange =
    me?.permissions.includes(Permission.CreateTasq) || false;

  return tasqsFetching || permissionsFetching ? (
    <CircularProgressCentered />
  ) : (
    <TasqPage
      tasq={data!}
      updateTasq={updateTasq}
      hasPermissionToChange={hasPermissionToChange}
    />
  );
};

export default TasqPageContainer;
