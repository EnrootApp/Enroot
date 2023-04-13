import { Tenant } from "../../../domain/tenant/Tenant";
import { CreateTenantForm } from "../../components/CreateTenant/CreateTenantContainer.types";
import { apiSlice } from "./apiSlice";

export const tenantApi = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    createTenant: builder.mutation<Tenant, CreateTenantForm>({
      query: ({ logoUrl, name }) => ({
        url: "/tenant",
        method: "POST",
        body: { logoUrl, name },
      }),
    }),
    getTenants: builder.query<Tenant[], { name: string }>({
      query: ({ name }) => ({
        url: "/tenant",
        params: { name },
      }),
    }),
    getCurrentTenant: builder.query<Tenant, {}>({
      query: () => {
        const pathname = window.location.pathname;
        const regex = /^\/tenant\/([^/]+)/;
        const match = pathname.match(regex);
        const tenantName = match?.[1] || "";

        return {
          url: "/tenant",
          params: { name: tenantName },
        };
      },
      transformResponse: (response: Tenant[], meta, arg) => response.shift()!,
    }),
    deleteTenant: builder.mutation<Tenant, {}>({
      query: ({}) => ({
        url: "/tenant",
        method: "DELETE",
      }),
    }),
    updateTenant: builder.mutation<Tenant, { logoUrl: string }>({
      query: ({ logoUrl }) => ({
        url: "/tenant",
        body: { logoUrl },
        method: "PUT",
      }),
    }),
  }),
});

export const {
  useCreateTenantMutation,
  useLazyGetTenantsQuery,
  useGetTenantsQuery,
  useUpdateTenantMutation,
  useDeleteTenantMutation,
  useGetCurrentTenantQuery,
} = tenantApi;
