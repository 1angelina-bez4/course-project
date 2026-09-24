using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    public partial class UserForm : Form
    {
        private DataTable usersTable;

        //таймер неактивности
        private System.Windows.Forms.Timer inactivityTimer;
        private const int TimeoutMinutes = 5; // Блокировка через 5 минут

        public UserForm()
        {
            InitializeComponent();

            // событие изменения выделения
            this.dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            //форматирование ячеек для маскирования данных в таблице
            this.dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            // Настройка таймера неактивности
            inactivityTimer = new System.Windows.Forms.Timer();
            inactivityTimer.Interval = TimeoutMinutes * 60 * 1000;
            inactivityTimer.Tick += InactivityTimer_Tick;
            ResetInactivityTimer();
        }

        // Перехват любых действий пользователя (мышь, клавиатура) для сброса таймера
        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_KEYDOWN = 0x0100;
            const int WM_LBUTTONDOWN = 0x0201;

            if (m.Msg == WM_MOUSEMOVE || m.Msg == WM_KEYDOWN || m.Msg == WM_LBUTTONDOWN)
            {
                ResetInactivityTimer();
            }
            base.WndProc(ref m);
        }

        private void ResetInactivityTimer()
        {
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            MessageBox.Show("Сессия завершена из-за неактивности в целях безопасности.",
                "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            new Auth().ShowDialog();
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetPlaceholders(
                (txtSearch, "Поиск"),
                (txtSurname, "Фамилия"),
                (txtName, "Имя"),
                (txtPatronymic, "Отчество"),
                (txtLogin, "Логин"),
                (txtPassword, "Пароль")
            );

            ComboBoxStyleHelper.Apply(cbSort, cbFilt, cbRole);
            LoadComboBoxes();
            LoadData();
        }

        private void LoadComboBoxes()
        {
            //Сортировка
            cbSort.Items.Clear();
            cbSort.Items.AddRange(new string[] { "По умолчанию", "Фамилия (А-Я)", "Фамилия (Я-А)" });
            cbSort.SelectedIndex = 0;

            //Фильтр
            cbFilt.Items.Clear();
            cbFilt.Items.AddRange(new string[] { "Все роли", "Администратор", "Менеджер", "Клиент" });
            cbFilt.SelectedIndex = 0;

            //Роль
            cbRole.Items.Clear();
            cbRole.Items.Add("Выберите роль...");

            try
            {
                using (MySqlConnection con = new MySqlConnection(new ClassConnection().ConnectString))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM role ORDER BY Name", con))
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cbRole.Items.Add(rdr["Name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки ролей: " + ex.Message);
            }
            cbRole.SelectedIndex = 0;
        }

        private void LoadData()
        {
            ClassConnection classConnection = new ClassConnection();
            try
            {
                using (MySqlConnection con = new MySqlConnection(classConnection.ConnectString))
                {
                    con.Open();

                    string selectQuery = @"SELECT u.Surname AS 'Фамилия',
	                                                u.Name AS 'Имя',
	                                                u.Patronymic AS 'Отчество',
	                                                u.PhoneNumber AS 'Номер Телефона',
	                                                u.Login AS 'Логин',
	                                                u.Password AS 'Пароль',
	                                                r.Name AS 'Роль'
	                                         FROM user u
                                         JOIN role r ON r.idRole = u.idRole;";

                    using (MySqlDataAdapter ad = new MySqlDataAdapter(selectQuery, con))
                    {
                        usersTable = new DataTable();
                        ad.Fill(usersTable);
                    }

                    dataGridView1.DataSource = usersTable;
                    GridStyle.Apply(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //маскирование персональных данных
        // Функция превращает "89021234567" в "8902****67"
        private string MaskPhone(string phone)
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

        // Маскирует пароль: показывает фиксированное количество звездочек
        private string MaskPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return "";

            // Фиксированное количество звездочек для безопасности
            return "••••••••";
        }

        // Применяет маскирование к ячейкам таблицы
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Маскирование телефона
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Номер Телефона" && e.Value != null)
            {
                e.Value = MaskPhone(e.Value.ToString());
                e.FormattingApplied = true;
            }

            // Маскирование пароля
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Пароль" && e.Value != null)
            {
                e.Value = MaskPassword(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;

                txtSurname.Text = Convert.ToString(row.Cells["Фамилия"].Value);
                txtName.Text = Convert.ToString(row.Cells["Имя"].Value);
                txtPatronymic.Text = Convert.ToString(row.Cells["Отчество"].Value);

                // Применяем маскирование к полю телефона при выборе строки
                txtPhone.Text = MaskPhone(Convert.ToString(row.Cells["Номер Телефона"].Value));

                txtLogin.Text = Convert.ToString(row.Cells["Логин"].Value);

                // Пароль тоже можно маскировать звездочками для безопасности
                string pass = Convert.ToString(row.Cells["Пароль"].Value);
                txtPassword.Text = string.IsNullOrEmpty(pass) ? "" : new string('*', pass.Length);

                string role = Convert.ToString(row.Cells["Роль"].Value);
                if (!string.IsNullOrEmpty(role) && cbRole.Items.Contains(role))
                {
                    cbRole.SelectedItem = role;
                }
                else
                {
                    cbRole.SelectedIndex = 0;
                }
            }
            else
            {
                ClearFields();
            }
        }

        private void ClearFields()
        {
            txtSurname.Clear();
            txtName.Clear();
            txtPatronymic.Clear();
            txtPhone.Clear();
            txtLogin.Clear();
            txtPassword.Clear();
            cbRole.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.Gray;

            if (cbRole.Items.Count > 0) cbRole.SelectedIndex = 0;
            if (cbFilt.Items.Count > 0) cbFilt.SelectedIndex = 0;
            if (cbSort.Items.Count > 0) cbSort.SelectedIndex = 0;

            if (usersTable != null)
            {
                usersTable.DefaultView.RowFilter = "";
                usersTable.DefaultView.Sort = "";
            }

            dataGridView1.DataSource = usersTable;
            ClearFields();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}