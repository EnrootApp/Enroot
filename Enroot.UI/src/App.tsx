import "./presentation/styles/index.css";

import { RouterProvider } from "react-router-dom";
import { router } from "./infrastructure/routing/router";
import { setLanguage } from "./presentation/localization/setLanguage";

const App = () => {
  setLanguage(localStorage.getItem("lang") || "ru");

  return <RouterProvider router={router} />;
};

export default App;
