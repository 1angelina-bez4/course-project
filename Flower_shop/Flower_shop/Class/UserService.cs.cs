using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Flower_shop
{
    // Класс хранения результата операции
    public class UserOperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string FailedField { get; set; }
    }

    /// <summary>
    /// Валидация
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Разрешает ввод только русских букв, пробела и дефиса
        /// </summary>
        public static void OnlyRussianLetters(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsLetter(e.KeyChar) &&
                Regex.IsMatch(e.KeyChar.ToString(), @"[а-яА-ЯёЁ]"))
                return;

            if (e.KeyChar == ' ' || e.KeyChar == '-')
                return;

            e.Handled = true;
        }

        /// <summary>
        /// Разрешает ввод только цифр
        /// </summary>
        public static void OnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Разрешает ввод только латинских букв и цифр
        /// </summary>
        public static void OnlyLatinAndDigits(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsLetterOrDigit(e.KeyChar) &&
                Regex.IsMatch(e.KeyChar.ToString(), @"[a-zA-Z0-9]"))
                return;

            e.Handled = true;
        }

        /// <summary>
        /// Проверяет русское имя/фамилию
        /// </summary>
        public static bool IsValidRussianName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(
                input.Trim(),
                @"^[а-яА-ЯёЁ\s-]{2,50}$");
        }

        /// <summary>
        /// Проверяет номер телефона.
        /// Разрешены только цифры, 10 или 11 знаков.
        /// </summary>
        public static bool IsValidPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string digits = Regex.Replace(input, @"\D", "");

            return Regex.IsMatch(digits, @"^\d{11}$");
        }
        /// <summary>
        /// Проверяет логин
        /// </summary>
        public static bool IsValidLogin(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(
                input.Trim(),
                @"^[a-zA-Z0-9_]{4,20}$");
        }
    }

    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Проверяет данные пользователя и сохраняет их в БД
        /// </summary>
        public UserOperationResult ValidateAndSave(
            UserModel user,
            bool isUpdate)
        {
            if (!ValidationHelper.IsValidRussianName(user.Surname))
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Фамилия должна содержать только русские буквы (от 2 до 50 символов).",
                    FailedField = "txtSurname"
                };
            }

            if (!ValidationHelper.IsValidRussianName(user.Name))
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Имя должно содержать только русские буквы.",
                    FailedField = "txtName"
                };
            }

            if (!ValidationHelper.IsValidPhone(user.PhoneNumber))
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Номер телефона должен содержать только цифры (10 или 11 знаков).",
                    FailedField = "txtPhone"
                };
            }

            if (!ValidationHelper.IsValidLogin(user.Login))
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Логин должен содержать только латинские буквы и цифры (от 4 до 20 символов).",
                    FailedField = "txtLogin"
                };
            }

            bool dbSuccess;

            if (isUpdate)
                dbSuccess = _repository.UpdateUser(user);
            else
                dbSuccess = _repository.AddUser(user);

            if (!dbSuccess)
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Не удалось сохранить данные. Проверьте подключение к базе данных или корректность логина.",
                    FailedField = null
                };
            }

            return new UserOperationResult
            {
                IsSuccess = true,
                Message = "Данные успешно сохранены!",
                FailedField = null
            };
        }
    }
}