import LocalizedStrings from "react-localization";

const apiStrings = new LocalizedStrings({
  en: {
    passwordChanged: "Your password has been changed successfully",
    settingsUpdated: "Settings saved",
  },
  ru: {
    passwordChanged: "Ваш пароль был изменен успешно",
    settingsUpdated: "Настройки сохранены",
  },
});

export default apiStrings;
