import { useEffect, useState } from "react";
import { useLazyGetTasqsQuery } from "../../state/api/tasqApi";
import TasqsPage from "../../../presentation/pages/Tasqs/TasqsPage";
import { GridFilterModel, GridPaginationModel } from "@mui/x-data-grid";

const TasqsPageContainer = () => {
  const [getTasqs, tasqs] = useLazyGetTasqsQuery();

  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
    page: 0,
    pageSize: 25,
  });
  const [filterModel, setFilterModel] = useState<GridFilterModel>({
    items: [],
    quickFilterValues: [],
  });

  useEffect(() => {
    if (tasqs.isUninitialized) {
      getTasqs({
        skip: paginationModel!.page * paginationModel!.pageSize,
        take: paginationModel!.pageSize,
      });
    }
  }, [tasqs.isSuccess]);

  const updatePaginationModel = (model: GridPaginationModel) => {
    setPaginationModel(model);
    getTasqs({
      title: filterModel.quickFilterValues![0],
      [filterModel.items[0]?.field]: filterModel.items[0]?.value,
      skip: model.page * model.pageSize,
      take: model.pageSize,
    });
  };

  const onFilterModelChange = (model: GridFilterModel) => {
    setFilterModel(filterModel);
    getTasqs({
      title: model.quickFilterValues![0],
      [model.items[0]?.field]: model.items[0]?.value,
      skip: paginationModel!.page * paginationModel!.pageSize,
      take: paginationModel!.pageSize,
    });
  };

  return (
    <TasqsPage
      tasqs={tasqs.data}
      isSuccess={tasqs.isSuccess}
      onFilterModelChange={onFilterModelChange}
      setPaginationModel={updatePaginationModel}
      paginationModel={paginationModel}
    />
  );
};

export default TasqsPageContainer;
