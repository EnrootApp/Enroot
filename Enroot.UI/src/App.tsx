import ForgotPassword from "./presentation/pages/ForgotPassword/ForgotPassword";
import LoginPage from "./presentation/pages/Login/LoginPage";
import RegisterPage from "./presentation/pages/Register/RegisterPage";
import ResetPassword from "./presentation/pages/ResetPassword/ResetPassword";

function App() {
  return (
    <ResetPassword
      formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
    />
  );
}

export default App;
