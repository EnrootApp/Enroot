import HomePage from "../../../presentation/pages/Home/HomePage";
import { useGetMeQuery } from "../../state/api/userApi";

const HomePageContainer = () => {
  const { data, isFetching } = useGetMeQuery();

  const isSystemAdmin = !isFetching && data?.role === "SystemAdmin";

  return <HomePage isSystemAdmin={isSystemAdmin} />;
};

export default HomePageContainer;
