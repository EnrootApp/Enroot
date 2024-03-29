export const routes = {
  login: "/login",
  register: "/register",
  forgotPassword: "/forgotPassword",
  resetPassword: "/resetPassword",
  home: "/home",
  profile: "/profile",
  tenant: "/tenant",
  tasqs: "tasqs",
  assignments: "assignments",
  accounts: "accounts",
  reports: "reports",
  tenantSettings: "settings",
};

export const publicRoutes = [
  routes.login,
  routes.register,
  routes.forgotPassword,
  routes.resetPassword,
];

export const shrinkRoutes = [
  routes.login,
  routes.register,
  routes.forgotPassword,
  routes.resetPassword,
  routes.profile,
  routes.tenantSettings,
];
