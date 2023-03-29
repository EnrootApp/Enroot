import apiStrings from "./apiMessages";
import errorStrings from "./errorMessages";
import strings from "./locales";

export const setLanguage = (value: string) => {
  strings.setLanguage(value);
  apiStrings.setLanguage(value);
  errorStrings.setLanguage(value);
};
