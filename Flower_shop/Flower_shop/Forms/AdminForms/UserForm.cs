using System;
using System.Collections.Generic;
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
        private UserService _userService;
        private int? currentUserId = null;
        private string _originalPhone = "";
        private string _originalPassword = "";
        private System.Windows.Forms.Timer inactivityTimer;
        private const int TimeoutMinutes = 5;
        private bool _isFiltering = false;

        public UserForm()
        {
            InitializeComponent();
            _userService = new UserService(new UserRepository(new ClassConnection().ConnectString));

            // Подписка на события
            this.dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            this.dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            this.butAdd.Click += butAdd_Click;
            this.butUpd.Click += butUpd_Click;
            this.txtSearch.TextChanged += txtSearch_TextChanged;
            this.cbFilt.SelectedIndexChanged += cbFilt_SelectedIndexChanged;
            this.cbSort.SelectedIndexChanged += cbSort_SelectedIndexChanged;

            this.txtPhone.Enter += txtPhone_Enter;
            this.txtPhone.Leave += txtPhone_Leave;
            this.txtPassword.Enter += txtPassword_Enter;
            this.txtPassword.Leave += txtPassword_Leave;

            // Таймер неактивности
            inactivityTimer = new System.Windows.Forms.Timer { Interval = TimeoutMinutes * 60 * 1000 };
            inactivityTimer.Tick += InactivityTimer_Tick;
            ResetInactivityTimer();

            // Валидация ввода (запрет символов)
            this.txtSurname.KeyPress += ValidationHelper.OnlyRussianLetters;
            this.txtName.KeyPress += ValidationHelper.OnlyRussianLetters;
            this.txtPatronymic.KeyPress += ValidationHelper.OnlyRussianLetters;
            this.txtPhone.KeyPress += ValidationHelper.OnlyDigits;
            this.txtLogin.KeyPress += ValidationHelper.OnlyLatinAndDigits;

            // 🆕 Проверка заполненности полей для активации кнопки "Добавить"
            this.txtSurname.TextChanged += CheckAddButtonState;
            this.txtName.TextChanged += CheckAddButtonState;
            this.txtLogin.TextChanged += CheckAddButtonState;
            this.txtPassword.TextChanged += CheckAddButtonState;
            this.txtPhone.TextChanged += CheckAddButtonState;
            this.cbRole.SelectedIndexChanged += CheckAddButtonState; // 🆕 Добавлена проверка при смене роли
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0200 || m.Msg == 0x0100 || m.Msg == 0x0201) ResetInactivityTimer();
            base.WndProc(ref m);
        }

        // 🆕 Метод проверки с правильными параметрами (object sender, EventArgs e)
        private void CheckAddButtonState(object sender, EventArgs e)
        {
            bool isSurnameValid = !string.IsNullOrWhiteSpace(txtSurname.Text) && txtSurname.Text != "Фамилия";
            bool isNameValid = !string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text != "Имя";
            bool isLoginValid = !string.IsNullOrWhiteSpace(txtLogin.Text) && txtLogin.Text != "Логин";
            bool isPasswordValid = !string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text != "Пароль";

            // Для телефона проверяем, введена ли хотя бы одна цифра
            bool isPhoneValid = txtPhone.Text.Any(char.IsDigit);

            // 🆕 Проверка роли: индекс должен быть больше 0 (0 - это "Выберите роль...")
            bool isRoleValid = cbRole.SelectedIndex > 0;

            // Кнопка активна ТОЛЬКО если все условия истинны
            butAdd.Enabled = isSurnameValid && isNameValid && isLoginValid && isPasswordValid && isPhoneValid && isRoleValid;
        }

        private void ResetInactivityTimer() { inactivityTimer.Stop(); inactivityTimer.Start(); }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            inactivityTimer.Stop();
            MessageBox.Show("Сессия завершена из-за неактивности (152-ФЗ).", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            new Auth().ShowDialog();
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetPlaceholders((txtSearch, "Поиск"), (txtSurname, "Фамилия"), (txtName, "Имя"),
                                          (txtPatronymic, "Отчество"), (txtLogin, "Логин"), (txtPassword, "Пароль"));
            ComboBoxStyleHelper.Apply(cbSort, cbFilt, cbRole);

            LoadComboBoxes();
            LoadData();

            BeginInvoke(new Action(() =>
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                ClearFields();
            }));
        }

        private void LoadComboBoxes()
        {
            cbSort.Items.AddRange(new string[] { "По умолчанию", "Фамилия (А-Я)", "Фамилия (Я-А)" });
            cbSort.SelectedIndex = 0;

            cbFilt.Items.AddRange(new string[] { "Все роли", "Администратор", "Менеджер", "Клиент" });
            cbFilt.SelectedIndex = 0;

            cbRole.Items.Add("Выберите роль...");
            try
            {
                using (var con = new MySqlConnection(new ClassConnection().ConnectString))
                {
                    con.Open();
                    using (var cmd = new MySqlCommand("SELECT Name FROM role ORDER BY Name", con))
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) cbRole.Items.Add(rdr["Name"].ToString());
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка загрузки ролей: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            cbRole.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                using (var con = new MySqlConnection(new ClassConnection().ConnectString))
                {
                    con.Open();
                    string query = @"SELECT u.idUser AS 'Id', u.Surname AS 'Фамилия', u.Name AS 'Имя',
                                            u.Patronymic AS 'Отчество', u.PhoneNumber AS 'Номер Телефона',
                                            u.Login AS 'Логин', u.Password AS 'Пароль', r.Name AS 'Роль'
                                     FROM user u JOIN role r ON r.idRole = u.idRole;";

                    using (var ad = new MySqlDataAdapter(query, con))
                    {
                        usersTable = new DataTable();
                        ad.Fill(usersTable);
                    }

                    dataGridView1.DataSource = usersTable;
                    GridStyle.Apply(dataGridView1);
                    if (dataGridView1.Columns["Id"] != null) dataGridView1.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return "";
            string digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length < 6) return digits;
            return digits.Substring(0, 4) + new string('*', digits.Length - 6) + digits.Substring(digits.Length - 2);
        }

        private string MaskPassword(string password) => string.IsNullOrWhiteSpace(password) ? "" : "••••••••";

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Номер Телефона" && e.Value != null)
            {
                e.Value = MaskPhone(e.Value.ToString());
                e.FormattingApplied = true;
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Пароль" && e.Value != null)
            {
                e.Value = MaskPassword(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private void ApplyFilters()
        {
            if (usersTable == null) return;

            _isFiltering = true;

            var view = usersTable.DefaultView;
            var filters = new List<string>();

            string search = txtSearch.Text == "Поиск" ? "" : txtSearch.Text.Trim().Replace("'", "''");
            if (!string.IsNullOrEmpty(search))
                filters.Add($"[Фамилия] LIKE '%{search}%' OR [Имя] LIKE '%{search}%' OR [Логин] LIKE '%{search}%'");

            string role = cbFilt.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(role) && role != "Все роли")
                filters.Add($"[Роль] = '{role.Replace("'", "''")}'");

            view.RowFilter = filters.Count > 0 ? string.Join(" AND ", filters) : "";

            if (cbSort.SelectedIndex == 1) view.Sort = "[Фамилия] ASC";
            else if (cbSort.SelectedIndex == 2) view.Sort = "[Фамилия] DESC";
            else view.Sort = "";

            dataGridView1.DataSource = view;

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;

            if (view.Count == 0)
            {
                ClearFields();
            }

            _isFiltering = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (_isFiltering) return;

            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow && dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.CurrentRow;
                currentUserId = Convert.ToInt32(row.Cells["Id"].Value);

                txtSurname.Text = Convert.ToString(row.Cells["Фамилия"].Value);
                txtName.Text = Convert.ToString(row.Cells["Имя"].Value);
                txtPatronymic.Text = Convert.ToString(row.Cells["Отчество"].Value);

                _originalPhone = Convert.ToString(row.Cells["Номер Телефона"].Value);
                _originalPassword = Convert.ToString(row.Cells["Пароль"].Value);

                txtPhone.Text = MaskPhone(_originalPhone);
                txtLogin.Text = Convert.ToString(row.Cells["Логин"].Value);
                txtPassword.Text = MaskPassword(_originalPassword);

                string role = Convert.ToString(row.Cells["Роль"].Value);
                cbRole.SelectedItem = (!string.IsNullOrEmpty(role) && cbRole.Items.Contains(role)) ? role : cbRole.Items[0];

                butUpd.Enabled = true;
            }
            else
            {
                butUpd.Enabled = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cbFilt_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_originalPhone)) txtPhone.SelectAll();
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text.Contains("*") || string.IsNullOrWhiteSpace(txtPhone.Text))
                txtPhone.Text = MaskPhone(_originalPhone);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_originalPassword)) txtPassword.SelectAll();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Contains("•"))
                txtPassword.Text = MaskPassword(_originalPassword);
        }

        private void RestorePlaceholders()
        {
            txtSurname.Text = "Фамилия"; txtSurname.ForeColor = Color.PaleVioletRed;
            txtName.Text = "Имя"; txtName.ForeColor = Color.PaleVioletRed;
            txtPatronymic.Text = "Отчество"; txtPatronymic.ForeColor = Color.PaleVioletRed;
            txtPhone.Clear();
            txtLogin.Text = "Логин"; txtLogin.ForeColor = Color.PaleVioletRed;
            txtPassword.Text = "Пароль"; txtPassword.ForeColor = Color.PaleVioletRed;
        }

        private void ClearFields()
        {
            currentUserId = null;
            _originalPhone = "";
            _originalPassword = "";
            RestorePlaceholders();
            cbRole.SelectedIndex = 0; // Сбрасываем роль на "Выберите роль..."
            butUpd.Enabled = false;
            butAdd.Enabled = false;

            // 🆕 Вызываем проверку состояния кнопки, передавая пустые аргументы
            CheckAddButtonState(this, EventArgs.Empty);
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            // Защитная проверка (на случай, если кнопку нажали программно или обошли UI)
            if (txtSurname.Text == "Фамилия" || txtName.Text == "Имя" || txtLogin.Text == "Логин" || txtPassword.Text == "Пароль" || cbRole.SelectedIndex == 0)
            {
                MessageBox.Show("Заполните все обязательные поля, включая роль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string phone = new string(txtPhone.Text.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Введите номер телефона!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            var user = new UserModel
            {
                Id = null,
                Surname = txtSurname.Text.Trim(),
                Name = txtName.Text.Trim(),
                Patronymic = txtPatronymic.Text == "Отчество" ? null : txtPatronymic.Text.Trim(),
                PhoneNumber = phone,
                Login = txtLogin.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                RoleName = cbRole.SelectedItem?.ToString() ?? "Клиент"
            };

            var result = _userService.ValidateAndSave(user, false);
            if (!result.IsSuccess) { MessageBox.Show(result.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); SetFocusToField(result.FailedField); return; }

            MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            ClearFields();
        }

        private void butUpd_Click(object sender, EventArgs e)
        {
            if (currentUserId == null)
            {
                MessageBox.Show("Выберите пользователя в таблице!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string phoneToSave = txtPhone.Text.Contains("*") ? _originalPhone : new string(txtPhone.Text.Where(char.IsDigit).ToArray());
            string passToSave = txtPassword.Text.Contains("•") ? _originalPassword : txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(phoneToSave))
            {
                MessageBox.Show("Введите номер телефона!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            var user = new UserModel
            {
                Id = currentUserId,
                Surname = txtSurname.Text.Trim(),
                Name = txtName.Text.Trim(),
                Patronymic = txtPatronymic.Text == "Отчество" ? null : txtPatronymic.Text.Trim(),
                PhoneNumber = phoneToSave,
                Login = txtLogin.Text.Trim(),
                Password = passToSave,
                RoleName = cbRole.SelectedItem?.ToString() ?? "Клиент"
            };

            var result = _userService.ValidateAndSave(user, true);
            if (!result.IsSuccess) { MessageBox.Show(result.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); SetFocusToField(result.FailedField); return; }

            MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            ClearFields();
        }

        private void SetFocusToField(string fieldName)
        {
            switch (fieldName)
            {
                case "txtSurname": txtSurname.Focus(); txtSurname.SelectAll(); break;
                case "txtName": txtName.Focus(); txtName.SelectAll(); break;
                case "txtPhone": txtPhone.Focus(); txtPhone.SelectAll(); break;
                case "txtLogin": txtLogin.Focus(); txtLogin.SelectAll(); break;
                case "txtPassword": txtPassword.Focus(); txtPassword.SelectAll(); break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск"; txtSearch.ForeColor = Color.PaleVioletRed;
            cbRole.SelectedIndex = 0; cbFilt.SelectedIndex = 0; cbSort.SelectedIndex = 0;

            if (usersTable != null)
            {
                usersTable.DefaultView.RowFilter = "";
                usersTable.DefaultView.Sort = "";
                dataGridView1.DataSource = usersTable;
            }
            ClearFields();
        }

        private void exit_Click(object sender, EventArgs e) => this.Close();
    }
}