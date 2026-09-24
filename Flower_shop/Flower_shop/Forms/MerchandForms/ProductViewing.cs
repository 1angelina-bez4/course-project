using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flower_shop
{
    public partial class ProductViewing : Form
    {
        private DataTable productsTable;
        private DataTable productsWithCategories;

        private string searchFilter = "";   // поиск
        private string categoryFilter = "";  // фильтр по категории
        private string orderBy = "";         // сортировка
        //пагинация
        int currentPage = 1;
        int pageSize = 15;
        int totalRecords = 0;
        int totalPages = 0;

        public ProductViewing()
        {
            InitializeComponent();

            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;

            txtSearch.Enter -= search_Enter;
            txtSearch.Enter += search_Enter;

            txtSearch.Leave -= search_Leave;
            txtSearch.Leave += search_Leave;

            linkLabel1.LinkClicked += linkLabel1_LinkClicked;

            //linkLabel1.Dock = DockStyle.Bottom;
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.Height = 30;
            linkLabel1.LinkColor = Color.PaleVioletRed;
            linkLabel1.ActiveLinkColor = Color.Red;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Поиск";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
            
        }

        private void search_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
        }
        //Поиск
        private void txtSearch_TextChanged(object sender, EventArgs e) 
        {
            currentPage = 1; 
            ApplyAll(); 
        }

        //Сортировка
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyAll();
        }
        //Фильтр по категории
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentPage = 1;

            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;

            cbSort.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;

            if (productsTable != null)
            {
                productsTable.DefaultView.RowFilter = "";
                productsTable.DefaultView.Sort = "";
            }

            ApplyAll();
        }

        private void ProductViewing_Load(object sender, EventArgs e)
        {
            // Поле поиска
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;
            cbSort.Items.Clear();
            // Сортировка
            cbSort.Items.Add("Сортировка по цене");
            cbSort.Items.Add("Цена: По Возрастанию");
            cbSort.Items.Add("Цена: По Убыванию");

            cbSort.SelectedIndex = 0;

            ComboBoxStyleHelper.Apply(cbSort);
            ComboBoxStyleHelper.Apply(cbCategory);

            // Загрузка данных
            LoadProducts();
            LoadCategories();
        }
        private void LoadProducts()
        {
            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();

                    string selectCmd = @"
                        SELECT 
                            p.Name AS 'Наименование',
                            p.Price AS 'Цена',
                            c.Name AS 'Название категории',
                            p.Description AS 'Описание'
                        FROM product p
                        JOIN categories c ON c.idCategories = p.idCategory;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd, con))
                    {
                        productsTable = new DataTable();
                        adapter.Fill(productsTable);
                    }

                    //Связь товар категория (для фильтра)
                    string queryCategories = @"
                        SELECT DISTINCT
                            p.Name AS 'Product',
                            c.Name AS 'Category'
                        FROM product p
                        JOIN categories c ON c.idCategories = p.idCategory;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(queryCategories, con))
                    {
                        productsWithCategories = new DataTable();
                        adapter.Fill(productsWithCategories);
                    }

                    dataGridView1.DataSource = productsTable;
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

        /// <summary>
        /// Загрузка Категорий в фильтры
        /// </summary>
        private void LoadCategories()
        {
            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();

                    cbCategory.Items.Clear();
                    cbCategory.Items.Add("Все категории");

                    using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM categories ORDER BY Name", con))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbCategory.Items.Add(reader["Name"].ToString());
                        }
                    }

                    cbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
            }
        }
        private void ApplyAll()
        {
            if (productsTable == null)
                return;

            DataView view = productsTable.DefaultView;

            List<string> filters = new List<string>();

            // ПОИСК
            string search = txtSearch.Text.Trim();

            if (search == "Поиск")
                search = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace("'", "''");

                filters.Add(
                    $"[Наименование] LIKE '%{search}%'"
                );
            }

            // ФИЛЬТР ПО КАТЕГОРИИ
            string category = cbCategory.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(category) &&
                category != "Все категории")
            {
                category = category.Replace("'", "''");

                var productsInCategory = productsWithCategories
                    .AsEnumerable()
                    .Where(row => row.Field<string>("Category") == category)
                    .Select(row => row.Field<string>("Product"))
                    .ToList();

                if (productsInCategory.Count == 0)
                {
                    label2.Text = "Количество записей: 0";
                    linkLabel1.Text = "";
                    dataGridView1.DataSource = productsTable.Clone();
                    return;
                }

                var names = productsInCategory
                    .Select(name => $"'{name.Replace("'", "''")}'");

                filters.Add(
                    $"[Наименование] IN ({string.Join(", ", names)})"
                );
            }
            // ПРИМЕНЕНИЕ ФИЛЬТРА

            view.RowFilter = filters.Count > 0
                ? string.Join(" AND ", filters)
                : "";

            // СОРТИРОВКА

            if (cbSort.SelectedIndex == 1)
            {
                view.Sort = "[Цена] ASC";
            }
            else if (cbSort.SelectedIndex == 2)
            {
                view.Sort = "[Цена] DESC";
            }
            else
            {
                view.Sort = "";
            }

            // КОЛИЧЕСТВО ЗАПИСЕЙ

            totalRecords = view.Count;

            label2.Text = $"Количество записей: {totalRecords}";

            // КОЛИЧЕСТВО СТРАНИЦ

            totalPages = (int)Math.Ceiling(
                (double)totalRecords / pageSize
            );

            if (totalPages == 0)
                totalPages = 1;

            if (currentPage > totalPages)
                currentPage = totalPages;

            if (currentPage < 1)
                currentPage = 1;

            // ПОЛУЧАЕМ ТОВАРЫ ТЕКУЩЕЙ СТРАНИЦЫ
            DataTable pageTable = productsTable.Clone();

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(
                startIndex + pageSize,
                view.Count
            );

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(view[i].Row);
            }

            dataGridView1.DataSource = pageTable;

            // СТРАНИЦЫ

            DisplayPageLinks();
        }
        private void DisplayPageLinks()
        {
            linkLabel1.Links.Clear();

            if (totalPages <= 1)
            {
                linkLabel1.Text = "";
                return;
            }

            string text = "";

            int startPage = Math.Max(1, currentPage - 3);
            int endPage = Math.Min(totalPages, currentPage + 3);

            if (startPage > 1)
                text += "... ";

            for (int i = startPage; i <= endPage; i++)
            {
                if (i == currentPage)
                    text += $"[{i}] ";
                else
                    text += $"{i} ";
            }

            if (endPage < totalPages)
                text += "...";

            linkLabel1.Text = text;

            // Добавляем ссылки
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == currentPage)
                    continue;

                string pageText = $"{i}";

                int index = text.IndexOf(pageText);

                if (index >= 0)
                {
                    linkLabel1.Links.Add(
                        index,
                        pageText.Length,
                        i
                    );
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData is int page)
            {
                currentPage = page;
                ApplyAll();
            }
        }
    }
}
