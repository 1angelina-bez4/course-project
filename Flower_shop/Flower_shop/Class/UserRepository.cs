using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    // Модель данных пользователя
    public class UserModel
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }

    // Единый класс для всей логики работы с пользователями
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Отображение данных из БД
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllUsers()
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                con.Open();
                string query = @"SELECT u.idUser AS 'Id', u.Surname AS 'Фамилия', u.Name AS 'Имя',
                                        u.Patronymic AS 'Отчество', u.PhoneNumber AS 'Номер Телефона',
                                        u.Login AS 'Логин', u.Password AS 'Пароль', r.Name AS 'Роль'
                                 FROM user u JOIN role r ON r.idRole = u.idRole;";

                using (MySqlDataAdapter ad = new MySqlDataAdapter(query, con))
                {
                    DataTable table = new DataTable();
                    ad.Fill(table);
                    return table;
                }
            }
        }
        /// <summary>
        /// Загрузка  ролей из БД
        /// </summary>
        /// <returns></returns>
        public List<string> GetRoleNames()
        {
            List<string> roles = new List<string>();
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM role ORDER BY Name", con))
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read()) roles.Add(rdr["Name"].ToString());
                }
            }
            return roles;
        }

        /// <summary>
        /// Добавление Пользователей в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(UserModel user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    int roleId = GetRoleIdByName(con, user.RoleName);
                    string query = @"INSERT INTO user (Surname, Name, Patronymic, PhoneNumber, Login, Password, idRole) 
                                     VALUES (@Surname, @Name, @Patronymic, @PhoneNumber, @Login, @Password, @RoleId)";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Surname", user.Surname);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Patronymic", string.IsNullOrEmpty(user.Patronymic) ? (object)DBNull.Value : user.Patronymic);
                        cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(user.PhoneNumber) ? (object)DBNull.Value : user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Login", user.Login);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("Ошибка добавления: " + ex.Message); return false; }
        }

        /// <summary>
        /// Запрос для редактирования пользователя 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(UserModel user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();
                    int roleId = GetRoleIdByName(con, user.RoleName);
                    string query = @"UPDATE user SET Surname = @Surname, Name = @Name, Patronymic = @Patronymic, 
                                         PhoneNumber = @PhoneNumber, Login = @Login, Password = @Password, idRole = @RoleId 
                                     WHERE idUser = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.Parameters.AddWithValue("@Surname", user.Surname);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Patronymic", string.IsNullOrEmpty(user.Patronymic) ? (object)DBNull.Value : user.Patronymic);
                        cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(user.PhoneNumber) ? (object)DBNull.Value : user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Login", user.Login);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("Ошибка обновления: " + ex.Message); return false; }
        }

        /// <summary>
        /// Отображение ролей в списке
        /// </summary>
        /// <param name="con"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private int GetRoleIdByName(MySqlConnection con, string roleName)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT idRole FROM role WHERE Name = @Name", con))
            {
                cmd.Parameters.AddWithValue("@Name", roleName);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 1;
            }
        }

     /// <summary>
     /// Маскировка данных
     /// </summary>
     /// <param name="phone"></param>
     /// <returns></returns>
        public static string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return "";
            string digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length < 6) return digits;

            int keepStart = 4;
            int keepEnd = 2;
            string start = digits.Substring(0, keepStart);
            string end = digits.Substring(digits.Length - keepEnd);
            string mask = new string('*', digits.Length - keepStart - keepEnd);

            return start + mask + end;
        }

        public static string MaskPassword(string password)
        {
            return string.IsNullOrWhiteSpace(password) ? "" : "••••••••";
        }

        /// <summary>
        /// Фильтрация и сортировка
        /// </summary>
        /// <param name="view"></param>
        /// <param name="searchText">переменная для хранения поиска</param>
        /// <param name="roleFilter">переменная для хранения выбранного роли пользователя</param>
        /// <param name="sortOption">переменная для хранения сортировки</param>
        public static void ApplyFilters(DataView view, string searchText, string roleFilter, string sortOption)
        {
            view.RowFilter = "";

            if (!string.IsNullOrEmpty(searchText))
            {
                string safeSearch = searchText.Replace("'", "''");
                view.RowFilter = $"[Фамилия] LIKE '%{safeSearch}%' OR [Имя] LIKE '%{safeSearch}%' OR [Логин] LIKE '%{safeSearch}%'";
            }

            if (roleFilter != "Все роли")
            {
                string safeRole = roleFilter.Replace("'", "''");
                string roleCondition = $"[Роль] = '{safeRole}'";
                view.RowFilter = string.IsNullOrEmpty(view.RowFilter)
                    ? roleCondition
                    : $"{view.RowFilter} AND {roleCondition}";
            }

            if (sortOption == "Фамилия (А-Я)")
            {
                view.Sort = "[Фамилия] ASC";
            }
            else if (sortOption == "Фамилия (Я-А)")
            {
                view.Sort = "[Фамилия] DESC";
            }
            else
            {
                view.Sort = "";
            }
        }
    }
}