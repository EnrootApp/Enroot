import LocalizedStrings from "react-localization";

const errorStrings = new LocalizedStrings({
  enUS: {
    notEmpty: "Field must not be empty",
    invalidEmail: "Enter valid email",
    tooShort: "Should contain more then {0} characters",
    characters: "Should contain at least one alphabetic character",
    fileToBig: "Selected file is to big",
    tenantName:
      "Tenant Name can contain at least 3 numeric or latin alphabetic characters",
    dateAfter: "The specified date must be after initial",
    dateRange: "Date range must not exceed {0} days",
  },
  ruRU: {
    notEmpty: "Поле должно быть заполнено",
    invalidEmail: "Некорректный адрес электронной почты",
    tooShort: "Поле не должно быть короче {0} символов",
    characters: "Поле должно содержать хотя бы одну букву",
    fileToBig: "Выбранный файл слишком велик",
    tenantName:
      "Название организации может состоять только из цифр и латинских букв и должно включать не менее 3 символов",
    dateAfter: "Указанная дата должна быть позже начальной",
    dateRange: "Временной период не должен превышать {0} дней",
  },
});

export default errorStrings;
