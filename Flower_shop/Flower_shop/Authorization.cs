using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    public partial class Auth : Form
    {
        int NumOfAttemp = 0;
        bool isBlocked = false;        
        string captchaCode = "";      
        Random rand = new Random();

        public Auth()
        {
            InitializeComponent();
            Log_In.Enabled = false;
            InputLogin.TextChanged += (s, e) => CheckFields();
            InputPasswd.TextChanged += (s, e) => CheckFields();

            captcha.Visible = false;
            InputCaptcha.Visible = false;
            InputCaptcha.TextChanged += (s, e) => CheckFields();

        }

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        private void CheckFields()
        {
            string login = InputLogin.Text.Trim();
            string password = InputPasswd.Text.Trim();

            bool filled = !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);

            
            if (captcha.Visible)
            {
                filled = filled && !string.IsNullOrEmpty(InputCaptcha.Text.Trim());
            }

            Log_In.Enabled = filled && !isBlocked;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик открытия формы завизимости от id
        /// </summary>
        /// <param name="roleId">получаем id роли от пользователя</param>
        private void OpenFormByRole( int roleId, int userId)
        {
            this.Hide();

            switch (roleId)
            {
                case 1:
                    new MenuForDirector(userId).ShowDialog();
                    break;
                case 2:
                    new MenuForAdministrator(userId).ShowDialog();
                    break;
                case 3:
                    new MenuForMerchandiseSpecialist(userId).ShowDialog();
                    break;
                default:
                    new MenuForSalesman(userId).ShowDialog();
                    break;
            }

            this.Close();
        }


        /// <summary>
        /// Генерация captcha
        /// </summary>
        private void GenerateCaptcha()
        {

            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";

            captchaCode = "";

            for (int i = 0; i < 5; i++)
            {
                captchaCode += chars[rand.Next(chars.Length)];
            }

            captcha.Text = captchaCode;
            captcha.Font = new Font("Monotype Corsiva", 20, FontStyle.Bold | FontStyle.Italic);
            captcha.ForeColor = Color.DarkRed;
            captcha.Visible = true;
;

            InputCaptcha.Visible = true;
            InputCaptcha.Clear();
        }
        /// <summary>
        /// Блокировка кнопок
        /// </summary>
        private async void BlockForTenSeconds()
        {
            isBlocked = true;
            Log_In.Enabled = false;
            
            timeText.Visible = true;
            timeText.Refresh();

            for (int i = 10; i > 0; i--)
            {
                timeText.Text = $"Подождите {i} сек...";
                await System.Threading.Tasks.Task.Delay(1000);
            }
            timeText.Visible = false;
            isBlocked = false;

            GenerateCaptcha();
            CheckFields();
        }

        private void Log_In_Click(object sender, EventArgs e)
        {
            if (isBlocked) return;

            string login = InputLogin.Text.Trim();
            string password = InputPasswd.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Пожалуйста, введите логин и пароль",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (captcha.Visible)
            {
                string userCaptcha = InputCaptcha.Text.Trim();
                if (!string.Equals(userCaptcha, captchaCode, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "Неверная капча! Вход заблокирован на 10 секунд.",
                        "Ошибка капчи",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    NumOfAttemp++;
                    BlockForTenSeconds();
                    return;
                }
            }

            ClassConnection classConnection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(classConnection.ConnectString))
                {
                    con.Open();

                    string commandSql = $"SELECT idUser, idRole FROM user WHERE Login = '{login}' AND Password = '{password}'";

                    using (MySqlCommand cmd = new MySqlCommand(commandSql, con))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("idUser");
                            int roleId = reader.GetInt32("idRole");
                            if (captcha.Visible)
                            {
                                string userCaptcha = InputCaptcha.Text.Trim();

                                if (!string.Equals(userCaptcha, captchaCode, StringComparison.OrdinalIgnoreCase))
                                {
                                    MessageBox.Show(
                                        "Неверная капча! Вход заблокирован на 10 секунд.",
                                        "Ошибка капчи",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    );

                                    NumOfAttemp++;
                                    BlockForTenSeconds();
                                    return;
                                }
                            }
                            OpenFormByRole(roleId, userId);
                        }
                        else
                        {
                            NumOfAttemp++;

                            MessageBox.Show(
                                "Неверный логин или пароль",
                                "Ошибка входа",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );

                            InputLogin.Clear();
                            InputPasswd.Clear();
                            InputLogin.Focus();

                            if (NumOfAttemp >= 2)
                            {
                                GenerateCaptcha();
                            }

                            CheckFields(); ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NumOfAttemp++;
            }
        }

        public void Captcha( int NumOfAttemp)
        {
            
        }
    }
}
