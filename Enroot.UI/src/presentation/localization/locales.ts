import LocalizedStrings from "react-localization";

const strings = new LocalizedStrings({
  en: {
    signIn: "Sign In",
    signUp: "Sign Up",
    email: "Email",
    password: "Password",
    submit: "Submit",
    haveAccount: "Already have an account?",
    dontHaveAccount: "Don't have an account?",
    forgotPassword: "Forgot password?",
  },
  ru: {
    signIn: "Вход",
    signUp: "Регистрация",
    email: "Адрес электронной почты",
    password: "Пароль",
    submit: "Войти",
    haveAccount: "Уже есть аккаунт?",
    dontHaveAccount: "Еще нет аккаунта?",
    forgotPassword: "Забыли пароль?",
  },
});

export default strings;
