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
  }),
});

export const { useCreateTenantMutation } = tenantApi;
