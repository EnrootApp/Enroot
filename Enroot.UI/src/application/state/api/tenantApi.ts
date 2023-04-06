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
  }),
});

export const {
  useCreateTenantMutation,
  useLazyGetTenantsQuery,
  useGetTenantsQuery,
} = tenantApi;
