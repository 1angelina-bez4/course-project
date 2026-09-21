using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flower_shop
{
    public partial class SupplierSearch : Form
    {
        private DataTable suppliersTable;
        private DataTable suppliersWithCategories;

        public SupplierSearch()
        {
            InitializeComponent();
            
        }

        private void SupplierSearch_Load(object sender, EventArgs e)
        {
            // Placeholder
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;
            

            // Сортировка
            cbSort.Items.Clear();
            cbSort.Items.Add("Название фирмы: А-Я");
            cbSort.Items.Add("Название фирмы: Я-А");
            ComboBoxStyleHelper.Apply(cbSort);
            cbSort.SelectedIndex = 0;

            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();
                    LoadCategories(con);

                    string selectCmd = @"SELECT Address AS 'Адрес',
                                                ContactPerson AS 'Контактное лицо',
                                                NumberPhone AS 'Номер телефона',
                                                Email AS 'Email',
                                                SupplierCompany AS 'Название фирмы' 
                                         FROM Suppliers;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd, con))
                    {
                        suppliersTable = new DataTable();
                        adapter.Fill(suppliersTable);
                    }

                    string queryCategories = @"
                        SELECT DISTINCT
                            s.SupplierCompany AS 'Supplier',
                            c.Name AS 'Category'
                        FROM Suppliers s
                        JOIN Storehouse sh ON s.idSuppliers = sh.Suppliers_idSuppliers
                        JOIN Product p ON p.idProduct = sh.Product_idProduct
                        JOIN Categories c ON p.idCategory = c.idCategories;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(queryCategories, con))
                    {
                        suppliersWithCategories = new DataTable();
                        adapter.Fill(suppliersWithCategories);
                    }

                    dataGridView1.DataSource = suppliersTable;
                    GridStyle.Apply(dataGridView1);
                    ApplyAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories(MySqlConnection con)
        {
            cbCategory.Items.Clear();
            cbCategory.Items.Add("Все категории");

            ComboBoxStyleHelper.Apply(cbCategory);
            using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM Categories ORDER BY Name", con))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cbCategory.Items.Add(reader["Name"].ToString());
                }
            }

            cbCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Объединяет поиск + фильтрацию + сортировку
        /// </summary>
        private void ApplyAll()
        {
            if (suppliersTable == null || suppliersWithCategories == null) return;

            DataView view = suppliersTable.DefaultView;
            List<string> filters = new List<string>();

            //фильтр по категории
            string selectedCategory = cbCategory.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "Все категории")
            {
                var suppliersInCategory = suppliersWithCategories
                    .AsEnumerable()
                    .Where(row => row.Field<string>("Category") == selectedCategory)
                    .Select(row => row.Field<string>("Supplier"))
                    .ToList();

                if (suppliersInCategory.Count > 0)
                {
                    string catFilter = string.Join(" OR ",
                        suppliersInCategory.Select(s => $"[Название фирмы] = '{s.Replace("'", "''")}'"));
                    filters.Add($"({catFilter})");
                }
                else
                {
                    dataGridView1.DataSource = suppliersTable.Clone();
                    return;
                }
            }

            //поиск по названию
            string search = txtSearch.Text.Trim().Replace("'", "''");
            if (search == "Поиск") search = "";

            if (!string.IsNullOrEmpty(search))
            {
                filters.Add($"[Название фирмы] LIKE '%{search}%'");
            }

            //объединение фильтров
            view.RowFilter = filters.Count > 0 ? string.Join(" AND ", filters) : "";

            //сортировка
            if (cbSort.SelectedIndex == 0)
                view.Sort = "[Название фирмы] ASC";
            else if (cbSort.SelectedIndex == 1)
                view.Sort = "[Название фирмы] DESC";

            dataGridView1.DataSource = view;
        }

        //События
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyAll();
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e) => ApplyAll();
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyAll();

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Поиск";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
            ApplyAll();
        }

        private void exit_Click(object sender, EventArgs e) => this.Close();

        private void sbros_Click_1(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;

            if (cbCategory.Items.Count > 0)
                cbCategory.SelectedIndex = 0;   // "Все категории"

            if (cbSort.Items.Count > 0)
                cbSort.SelectedIndex = 0;       // "Название фирмы: А-Я"

            if (suppliersTable != null)
            {
                suppliersTable.DefaultView.RowFilter = "";
                suppliersTable.DefaultView.Sort = "";
            }

            dataGridView1.DataSource = suppliersTable;
        }
    }
}