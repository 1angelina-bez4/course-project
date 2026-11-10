using System;

namespace Flower_shop
{
    // Результат операции (остался здесь, так как используется сервисом пользователей)
    public class UserOperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string FailedField { get; set; }
    }

    /// <summary>
    /// Сервис для работы конкретно с Пользователями
    /// </summary>
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Проверяет данные пользователя через ValidationHelper и сохраняет их в БД
        /// </summary>
        public UserOperationResult ValidateAndSave(UserModel user, bool isUpdate)
        {
            // Мы просто вызываем методы из нашего нового универсального класса ValidationHelper
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
                    Message = "Номер телефона должен содержать 10 или 11 цифр.",
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

            bool dbSuccess = isUpdate ? _repository.UpdateUser(user) : _repository.AddUser(user);

            if (!dbSuccess)
            {
                return new UserOperationResult
                {
                    IsSuccess = false,
                    Message = "Не удалось сохранить данные. Проверьте подключение к базе данных.",
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