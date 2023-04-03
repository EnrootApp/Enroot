import { RouterProvider } from "react-router-dom";

import { router } from "./infrastructure/routing/router";
import { setLanguage } from "./presentation/localization/setLanguage";
import "./presentation/styles/index.css";

const App = () => {
  setLanguage(localStorage.getItem("lang") || "ru");

  return <RouterProvider router={router} />;
};

export default App;
