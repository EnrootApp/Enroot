import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { shrinkRoutes } from "../../../infrastructure/routing/routes";
import AppWindow from "../../../presentation/components/AppWindow/AppWindow";
import { AppWindowContainerProps } from "./AppWindowContainer.types";

const AppWindowContainer: React.FC<AppWindowContainerProps> = ({}) => {
  const [shrink, setShrink] = useState(false);

  const location = useLocation();
  const route = location.pathname.split("/").pop();

  useEffect(() => {
    setShrink(shrinkRoutes.includes(route!));
  }, [location]);

  return <AppWindow shrink={shrink} />;
};

export default AppWindowContainer;
