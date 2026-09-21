using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    /// <summary>
    /// Помощник для загрузки и отображения информации о пользователе
    /// </summary>
    public class UserInfoHelper
    {
        /// <summary>
        /// Загружает ФИО пользователя из БД по его ID
        /// </summary>
        /// <param name="userId">Передавыемый id пользователя</param>
        /// <returns>возращаем ФИО пользователя</returns>
        public static string GetUserFullName(int userId)
        {
            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();

                    string command = $"SELECT Surname, Name, Patronymic FROM user WHERE idUser = {userId};";

                    using (MySqlCommand cmd = new MySqlCommand(command, con))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string surname = reader.GetString("Surname");
                            string name = reader.GetString("Name");
                            string patronymic = reader.GetString("Patronymic");

                            return $"{surname} {name} {patronymic}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных пользователя: " + ex.Message,
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        /// <summary>
        /// Устанавливает в Label текст "Роль: ФИО"
        /// </summary>
        /// <param name="label">Label для отображения</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="roleTitle">Название роли </param>
        public static void SetUserLabel(Label label, int userId, string roleTitle)
        {
            string fullName = GetUserFullName(userId);

            if (fullName != null)
                label.Text = $"{roleTitle}: {fullName}";
            else
                label.Text = $"{roleTitle}: (не найден)";
        }
    }
}
