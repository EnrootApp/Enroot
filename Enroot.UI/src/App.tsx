import LoginPage from "./presentation/pages/Login/LoginPage";

function App() {
  return (
    <LoginPage
      formikConfig={{ initialValues: {}, onSubmit(values, formikHelpers) {} }}
    />
  );
}

export default App;
