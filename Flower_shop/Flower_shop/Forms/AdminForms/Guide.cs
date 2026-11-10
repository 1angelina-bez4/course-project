using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    public partial class Guide : Form
    {
        private DataTable suppliersTable;
        private DataTable categoriesTable;

        private int? currentSupplierId = null;
        private int? currentCategoryId = null;

        private readonly Color PlaceholderColor = Color.PaleVioletRed;
        private readonly Color InputColor = Color.Black;

        public Guide()
        {
            InitializeComponent();

            //События привязки данных
            this.dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            this.dataGridView2.DataBindingComplete += dataGridView2_DataBindingComplete;

            //События выбора строки
            this.dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;

            //События кнопок
            btnAddSup.Click += btnAddSup_Click;
            btnEdtSup.Click += btnEdtSup_Click;
            btnDelSup.Click += btnDelSup_Click;

            btnAddCat.Click += btnAddCat_Click;
            btnEdtCat.Click += btnEdtCat_Click;
            btnDelCat.Click += btnDelCat_Click;

            //Проверка полей при вводе
            txtSupplierCompany.TextChanged += CheckSupplierFields;
            txtContactPerson.TextChanged += CheckSupplierFields;
            txtPhone.TextChanged += CheckSupplierFields;
            txtEmail.TextChanged += CheckSupplierFields;
            txtAddress.TextChanged += CheckSupplierFields;

            txtCategoryName.TextChanged += CheckCategoryFields;
            txtCategoryDescription.TextChanged += CheckCategoryFields;

            //События для умных подсказок (Placeholder) - Поставщики
            txtSupplierCompany.Enter += Txt_Enter; txtSupplierCompany.Leave += Txt_Leave;
            txtContactPerson.Enter += Txt_Enter; txtContactPerson.Leave += Txt_Leave;
            txtPhone.Enter += Txt_Enter; txtPhone.Leave += Txt_Leave;
            txtEmail.Enter += Txt_Enter; txtEmail.Leave += Txt_Leave;
            txtAddress.Enter += Txt_Enter; txtAddress.Leave += Txt_Leave;

            //События для умных подсказок (Placeholder) - Категории
            txtCategoryName.Enter += Txt_Enter; txtCategoryName.Leave += Txt_Leave;
            txtCategoryDescription.Enter += Txt_Enter; txtCategoryDescription.Leave += Txt_Leave;

            //Загрузка при старте
            this.Load += Guide_Load;
        }

        private void Guide_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData2();

            BeginInvoke(new Action(() =>
            {
                RestoreAllPlaceholders();
                btnAddSup.Enabled = false; btnEdtSup.Enabled = false; btnDelSup.Enabled = false;
                btnAddCat.Enabled = false; btnEdtCat.Enabled = false; btnDelCat.Enabled = false;
            }));
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection(); dataGridView1.CurrentCell = null;
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection(); dataGridView2.CurrentCell = null;
        }


        private void Txt_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.ForeColor == PlaceholderColor)
            {
                txt.Text = "";
                txt.ForeColor = InputColor;
            }
        }

        private void Txt_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                RestorePlaceholder(txt);
            }
        }

        private void RestorePlaceholder(TextBox txt)
        {
            txt.ForeColor = PlaceholderColor;
            switch (txt.Name)
            {
                case "txtSupplierCompany": txt.Text = "Наименование фирмы"; break;
                case "txtContactPerson": txt.Text = "Контактное лицо"; break;
                case "txtPhone": txt.Text = "Номер телефона"; break;
                case "txtEmail": txt.Text = "Почта"; break;
                case "txtAddress": txt.Text = "Адрес"; break;
                case "txtCategoryName": txt.Text = "Наименование"; break;
                case "txtCategoryDescription": txt.Text = "Описание"; break;
            }
        }

        private void RestoreAllPlaceholders()
        {
            RestorePlaceholder(txtSupplierCompany); RestorePlaceholder(txtContactPerson);
            RestorePlaceholder(txtPhone); RestorePlaceholder(txtEmail); RestorePlaceholder(txtAddress);
            RestorePlaceholder(txtCategoryName); RestorePlaceholder(txtCategoryDescription);
        }

        private bool IsFieldFilled(TextBox txt, string placeholderText)
        {
            return txt.ForeColor == InputColor && !string.IsNullOrWhiteSpace(txt.Text) && txt.Text != placeholderText;
        }

        private void LoadData()
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"SELECT idSuppliers AS 'Id', Address AS 'Адрес', ContactPerson AS 'Контактное лицо', 
                                      NumberPhone AS 'Номер телефона', Email AS 'Почта', SupplierCompany AS 'Наименование фирмы' 
                               FROM suppliers;";
                using (MySqlDataAdapter ad = new MySqlDataAdapter(cmd, con))
                {
                    suppliersTable = new DataTable(); ad.Fill(suppliersTable);
                }
                dataGridView1.DataSource = suppliersTable;
                GridStyle.Apply(dataGridView1);
                if (dataGridView1.Columns["Id"] != null) dataGridView1.Columns["Id"].Visible = false;
            }
        }

        private void LoadData2()
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"SELECT idCategories AS 'Id', Name AS 'Наименование', Description AS 'Описание' FROM categories;";
                using (MySqlDataAdapter ad = new MySqlDataAdapter(cmd, con))
                {
                    categoriesTable = new DataTable(); ad.Fill(categoriesTable);
                }
                dataGridView2.DataSource = categoriesTable;
                GridStyle.Apply(dataGridView2);
                if (dataGridView2.Columns["Id"] != null) dataGridView2.Columns["Id"].Visible = false;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                var row = dataGridView1.CurrentRow;
                currentSupplierId = Convert.ToInt32(row.Cells["Id"].Value);

                txtSupplierCompany.Text = Convert.ToString(row.Cells["Наименование фирмы"].Value); txtSupplierCompany.ForeColor = InputColor;
                txtContactPerson.Text = Convert.ToString(row.Cells["Контактное лицо"].Value); txtContactPerson.ForeColor = InputColor;
                txtPhone.Text = Convert.ToString(row.Cells["Номер телефона"].Value); txtPhone.ForeColor = InputColor;
                txtEmail.Text = Convert.ToString(row.Cells["Почта"].Value); txtEmail.ForeColor = InputColor;
                txtAddress.Text = Convert.ToString(row.Cells["Адрес"].Value); txtAddress.ForeColor = InputColor;

                btnAddSup.Enabled = false; btnEdtSup.Enabled = true; btnDelSup.Enabled = true;
            }
            else { ClearSupplierFields(); }
        }

        private void CheckSupplierFields(object sender, EventArgs e)
        {
            if (currentSupplierId != null) { btnAddSup.Enabled = false; return; }
            btnAddSup.Enabled = IsFieldFilled(txtSupplierCompany, "Наименование фирмы") &&
                                IsFieldFilled(txtContactPerson, "Контактное лицо") &&
                                IsFieldFilled(txtPhone, "Номер телефона") &&
                                IsFieldFilled(txtEmail, "Почта") &&
                                IsFieldFilled(txtAddress, "Адрес");
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            if (!ValidSupplier()) return;
            if (IsSupplierExists(txtSupplierCompany.Text.Trim()))
            { MessageBox.Show("Поставщик с таким наименованием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"INSERT INTO suppliers (SupplierCompany, ContactPerson, NumberPhone, Email, Address) VALUES (@Company, @Contact, @Phone, @Email, @Address)";
                using (MySqlCommand command = new MySqlCommand(cmd, con))
                {
                    command.Parameters.AddWithValue("@Company", txtSupplierCompany.Text.Trim());
                    command.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim());
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Поставщик успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearSupplierFields(); LoadData();
        }

        private void btnEdtSup_Click(object sender, EventArgs e)
        {
            if (currentSupplierId == null) return;
            if (!ValidSupplier()) return;

            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"UPDATE suppliers SET SupplierCompany = @Company, ContactPerson = @Contact, NumberPhone = @Phone, Email = @Email, Address = @Address WHERE idSuppliers = @Id";
                using (MySqlCommand command = new MySqlCommand(cmd, con))
                {
                    command.Parameters.AddWithValue("@Id", currentSupplierId);
                    command.Parameters.AddWithValue("@Company", txtSupplierCompany.Text.Trim());
                    command.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim());
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Данные поставщика обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearSupplierFields(); LoadData();
        }

        private void btnDelSup_Click(object sender, EventArgs e)
        {
            if (currentSupplierId == null) return;
            if (MessageBox.Show("Удалить этого поставщика?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClassConnection connect = new ClassConnection();
                using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
                {
                    con.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM suppliers WHERE idSuppliers = @Id", con))
                    {
                        command.Parameters.AddWithValue("@Id", currentSupplierId);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Поставщик удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearSupplierFields(); LoadData();
            }
        }

        private void ClearSupplierFields()
        {
            currentSupplierId = null;
            RestoreAllPlaceholders();
            btnAddSup.Enabled = false; btnEdtSup.Enabled = false; btnDelSup.Enabled = false;
            dataGridView1.ClearSelection(); dataGridView1.CurrentCell = null;
        }

        private bool IsSupplierExists(string companyName)
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM suppliers WHERE SupplierCompany = @Name", con))
                {
                    command.Parameters.AddWithValue("@Name", companyName);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }


        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0 && dataGridView2.CurrentRow != null && !dataGridView2.CurrentRow.IsNewRow)
            {
                var row = dataGridView2.CurrentRow;
                currentCategoryId = Convert.ToInt32(row.Cells["Id"].Value);

                txtCategoryName.Text = Convert.ToString(row.Cells["Наименование"].Value); txtCategoryName.ForeColor = InputColor;
                txtCategoryDescription.Text = Convert.ToString(row.Cells["Описание"].Value); txtCategoryDescription.ForeColor = InputColor;

                btnAddCat.Enabled = false; btnEdtCat.Enabled = true; btnDelCat.Enabled = true;
            }
            else { ClearCategoryFields(); }
        }

        private void CheckCategoryFields(object sender, EventArgs e)
        {
            if (currentCategoryId != null) { btnAddCat.Enabled = false; return; }
            btnAddCat.Enabled = IsFieldFilled(txtCategoryName, "Наименование");
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            if (!ValidCategory()) return;
            if (IsCategoryExists(txtCategoryName.Text.Trim()))
            { MessageBox.Show("Категория с таким наименованием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"INSERT INTO categories (Name, Description) VALUES (@Name, @Description)";
                using (MySqlCommand command = new MySqlCommand(cmd, con))
                {
                    command.Parameters.AddWithValue("@Name", txtCategoryName.Text.Trim());
                    command.Parameters.AddWithValue("@Description", txtCategoryDescription.Text.Trim());
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Категория успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearCategoryFields(); LoadData2();
        }

        private void btnEdtCat_Click(object sender, EventArgs e)
        {
            if (currentCategoryId == null) return;
            if (!ValidCategory()) return;

            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"UPDATE categories SET Name = @Name, Description = @Description WHERE idCategories = @Id";
                using (MySqlCommand command = new MySqlCommand(cmd, con))
                {
                    command.Parameters.AddWithValue("@Id", currentCategoryId);
                    command.Parameters.AddWithValue("@Name", txtCategoryName.Text.Trim());
                    command.Parameters.AddWithValue("@Description", txtCategoryDescription.Text.Trim());
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Данные категории обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearCategoryFields(); LoadData2();
        }

        private void btnDelCat_Click(object sender, EventArgs e)
        {
            if (currentCategoryId == null) return;
            if (MessageBox.Show("Удалить эту категорию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClassConnection connect = new ClassConnection();
                using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
                {
                    con.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM categories WHERE idCategories = @Id", con))
                    {
                        command.Parameters.AddWithValue("@Id", currentCategoryId);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Категория удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCategoryFields(); LoadData2();
            }
        }

        private void ClearCategoryFields()
        {
            currentCategoryId = null;
            RestoreAllPlaceholders();
            btnAddCat.Enabled = false; btnEdtCat.Enabled = false; btnDelCat.Enabled = false;
            dataGridView2.ClearSelection(); dataGridView2.CurrentCell = null;
        }

        private bool IsCategoryExists(string categoryName)
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM categories WHERE Name = @Name", con))
                {
                    command.Parameters.AddWithValue("@Name", categoryName);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        /// <summary>
        /// Валидация полей
        /// </summary>
        /// <returns></returns>
        private bool ValidSupplier()
        {
            if (txtSupplierCompany.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtSupplierCompany.Text))
            { MessageBox.Show("Заполните поле 'Наименование фирмы'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSupplierCompany.Focus(); return false; }
            if (txtContactPerson.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtContactPerson.Text))
            { MessageBox.Show("Заполните поле 'Контактное лицо'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContactPerson.Focus(); return false; }
            if (txtPhone.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtPhone.Text))
            { MessageBox.Show("Заполните поле 'Номер телефона'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPhone.Focus(); return false; }
            if (txtEmail.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtEmail.Text))
            { MessageBox.Show("Заполните поле 'Почта'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEmail.Focus(); return false; }
            if (txtAddress.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtAddress.Text))
            { MessageBox.Show("Заполните поле 'Адрес'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtAddress.Focus(); return false; }

            if (!ValidationHelper.IsValidCompanyName(txtSupplierCompany.Text.Trim()))
            { MessageBox.Show("Наименование фирмы: от 2 до 100 символов (буквы, цифры, пробелы).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSupplierCompany.Focus(); txtSupplierCompany.SelectAll(); return false; }
            if (!ValidationHelper.IsValidRussianName(txtContactPerson.Text.Trim()))
            { MessageBox.Show("Контактное лицо: только русские буквы (2-50 символов).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContactPerson.Focus(); txtContactPerson.SelectAll(); return false; }
            if (!ValidationHelper.IsValidPhone(txtPhone.Text.Trim()))
            { MessageBox.Show("Телефон: ровно 10 или 11 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPhone.Focus(); txtPhone.SelectAll(); return false; }
            if (!ValidationHelper.IsValidEmail(txtEmail.Text.Trim()))
            { MessageBox.Show("Введите корректный Email (пример: info@company.ru).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEmail.Focus(); txtEmail.SelectAll(); return false; }

            return true;
        }

        private bool ValidCategory()
        {
            if (txtCategoryName.ForeColor == PlaceholderColor || string.IsNullOrWhiteSpace(txtCategoryName.Text))
            { MessageBox.Show("Заполните поле 'Наименование'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCategoryName.Focus(); return false; }
            if (!ValidationHelper.IsValidCategoryName(txtCategoryName.Text.Trim()))
            { MessageBox.Show("Наименование категории: от 2 до 50 символов (буквы и пробелы).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCategoryName.Focus(); txtCategoryName.SelectAll(); return false; }
            return true;
        }

        /// <summary>
        /// события при нажатия на кнопки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e) { this.Close(); }
        private void tabPage1_Click(object sender, EventArgs e) { LoadData(); BeginInvoke(new Action(() => ClearSupplierFields())); }
        private void tabPage2_Click(object sender, EventArgs e) { LoadData2(); BeginInvoke(new Action(() => ClearCategoryFields())); }
    }
}