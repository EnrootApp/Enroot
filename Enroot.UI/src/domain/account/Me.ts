import { Permission } from "../../application/common/enums/permission";

export interface Me {
  tenantId: string;
  id: string;
  userId: string;
  permissions: Permission[];
}
