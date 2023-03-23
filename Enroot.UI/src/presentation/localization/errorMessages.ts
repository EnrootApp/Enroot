import LocalizedStrings from "react-localization";

const errorStrings = new LocalizedStrings({
  en: {
    notEmpty: "Field must not be empty",
    invalidEmail: "Enter valid email",
    tooShort: "Should contain more then {0} characters",
    characters: "Should contain at least one alphabetic character",
  },
  ru: {
    notEmpty: "Поле должно быть заполнено",
    invalidEmail: "Некорректный адрес электронной почты",
    tooShort: "Поле не должно быть короче {0} символов",
    characters: "Поле должно содержать хотя бы одну букву",
  },
});

export default errorStrings;
