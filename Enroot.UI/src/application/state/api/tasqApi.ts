import { Tasq } from "../../../domain/tasq/Tasq";
import { TasqsFilters } from "../../pages/Tasqs/TasksPage.Types";
import { apiSlice } from "./apiSlice";

export const tasqsApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getTasqs: builder.query<Tasq[], TasqsFilters>({
      query: ({ title, creatorId, isAssigned, isCompleted, skip, take }) => ({
        url: "/tasq",
        params: { title, creatorId, isAssigned, isCompleted, skip, take },
      }),
    }),
  }),
});

export const { useLazyGetTasqsQuery } = tasqsApi;
