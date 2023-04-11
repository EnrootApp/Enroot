import { Attachment } from "../../../domain/tasq/Attachment";
import { Tasq } from "../../../domain/tasq/Tasq";
import { AddTasqForm } from "../../components/AddTasq/AddTasqContainer.types";
import { TasqsFilters } from "../../pages/Tasqs/TasksPage.types";
import { apiSlice } from "./apiSlice";

export const tasqsApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getTasqs: builder.query<Tasq[], TasqsFilters>({
      query: ({ title, creatorId, isAssigned, isCompleted, skip, take }) => ({
        url: "/tasq",
        params: { title, creatorId, isAssigned, isCompleted, skip, take },
      }),
    }),
    getTasq: builder.query<Tasq, { id: string }>({
      query: ({ id }) => ({
        url: `/tasq/${id}`,
      }),
    }),
    createTasq: builder.mutation<Tasq, AddTasqForm>({
      query: (form) => ({
        url: "/tasq",
        method: "POST",
        body: { ...form },
      }),
    }),
    updateTasq: builder.mutation<Tasq, { description: string; id: string }>({
      query: ({ description, id }) => ({
        url: `/tasq/${id}`,
        method: "PUT",
        body: { description },
      }),
    }),
    startTasq: builder.mutation<Tasq, { id: string }>({
      query: ({ id }) => ({
        url: `/tasq/start`,
        method: "POST",
        body: { assignmentId: id },
      }),
    }),
    completeTasq: builder.mutation<
      Tasq,
      { id: string; attachments: Attachment[] }
    >({
      query: ({ id, attachments }) => ({
        url: `/tasq/complete`,
        method: "POST",
        body: { assignmentId: id, attachments },
      }),
    }),
    approveTasq: builder.mutation<Tasq, { id: string }>({
      query: ({ id }) => ({
        url: `/tasq/approve`,
        method: "POST",
        body: { assignmentId: id },
      }),
    }),
    rejectTasq: builder.mutation<Tasq, { feedbackMessage: string; id: string }>(
      {
        query: ({ feedbackMessage, id }) => ({
          url: `/tasq/reject`,
          method: "POST",
          body: { assignmentId: id, feedbackMessage },
        }),
      }
    ),
    assignTasq: builder.mutation<Tasq, { tasqId: string; assigneeId: string }>({
      query: ({ tasqId, assigneeId }) => ({
        url: `/tasq/assign`,
        method: "POST",
        body: { tasqId, assigneeId },
      }),
    }),
  }),
});

export const {
  useLazyGetTasqsQuery,
  useCreateTasqMutation,
  useGetTasqQuery,
  useUpdateTasqMutation,
  useStartTasqMutation,
  useCompleteTasqMutation,
  useApproveTasqMutation,
  useRejectTasqMutation,
  useAssignTasqMutation,
} = tasqsApi;
