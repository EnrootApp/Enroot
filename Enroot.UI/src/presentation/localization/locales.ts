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
    forgotPasswordGuide:
      "To reset your password, please enter the email address associated with your account. We will send you instructions for resetting your password to the email address you provide.",
    backToLogin: "Back to Sign In page",
    resetPassword: "Reset password",
    resetPasswordGuide:
      "To reset your password, enter the code from the email you received and a new password.",
    securityCode: "Security code",
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
    forgotPasswordGuide:
      "Для восстановления пароля, пожалуйста, введите адрес электронной почты, который связан с вашей учетной записью. Мы отправим вам инструкции по восстановлению пароля на указанный адрес электронной почты.",
    backToLogin: "Вернуться ко входу",
    resetPassword: "Сброс пароля",
    resetPasswordGuide:
      "Чтобы сбросить пароль, введите код из письма, которое вы получили, и новый пароль",
    securityCode: "Код безопасности",
  },
});

export default strings;
