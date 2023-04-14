import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { shrinkRoutes } from "../../../infrastructure/routing/routes";
import AppWindow from "../../../presentation/components/AppWindow/AppWindow";
import { AppWindowContainerProps } from "./AppWindowContainer.types";

const AppWindowContainer: React.FC<AppWindowContainerProps> = ({}) => {
  const [shrink, setShrink] = useState(false);

  const location = useLocation();
  const route = location.pathname;

  useEffect(() => {
    setShrink(Boolean(shrinkRoutes.find((r) => route.includes(r))));
  }, [location]);

  return <AppWindow shrink={shrink} />;
};

export default AppWindowContainer;
