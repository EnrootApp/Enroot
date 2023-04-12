import { Role } from "../../common/enums/role";

export interface InviteAccountForm {
  email: string;
  roleId: Role;
}
