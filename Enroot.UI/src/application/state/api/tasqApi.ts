import { Tasq } from "../../../domain/tasq/Tasq";
import { AddTasqForm } from "../../components/AddTasq/AddTasqContainer.types";
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
    createTasq: builder.mutation<Tasq, AddTasqForm>({
      query: (form) => ({
        url: "/tasq",
        method: "POST",
        body: { ...form },
      }),
    }),
  }),
});

export const { useLazyGetTasqsQuery, useCreateTasqMutation } = tasqsApi;
