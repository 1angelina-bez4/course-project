using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel; // подключение к Excel 

namespace Flower_shop
{


    public partial class WarehouseInspection : Form
    {
        private DataTable storehouseTable;
        //private DataTable storehouseWithCategories;

        private string selectCmd = @"
                                    SELECT  
                                        sh.idStorehouse AS 'idСклада',
                                        sh.QuantityInStock AS 'Количество на складе',
                                        sh.NumberOfShippedItems AS 'Количество отгруженных товаров',
                                        sh.DeliveryDate AS 'Дата поставки',
                                        p.Name AS 'Название товара',
                                        sup.SupplierCompany AS 'Поставщик'
                                    FROM storehouse sh
                                    JOIN product p 
                                        ON sh.Product_idProduct = p.idProduct
                                    JOIN suppliers sup 
                                        ON sh.Suppliers_idSuppliers = sup.idSuppliers;";
        public WarehouseInspection()
        {
            InitializeComponent();

            txtSearch.TextChanged += txtSearch_TextChanged;
            nameFirmFiltr.SelectedIndexChanged += nameFirmFiltr_SelectedIndexChanged;
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            txtSearch.Click += txtSearch_Click;
            ComboBoxStyleHelper.Apply(nameFirmFiltr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Поиск";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
        }

        private void WarehouseInspection_Load(object sender, EventArgs e)
        {
            // Поле поиска
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;
            // Загрузка склада
            LoadProducts();

            // Загрузка поставщиков
            LoadSuppliers();
        }

        private void sbros_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.PaleVioletRed;

            nameFirmFiltr.SelectedIndex = 0;

            if (storehouseTable != null)
            {
                storehouseTable.DefaultView.RowFilter = "";
                storehouseTable.DefaultView.Sort = "";

                dataGridView1.DataSource = storehouseTable;
            }
        }

        private void LoadProducts()
        {
            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();

                    

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd, con))
                    {
                        storehouseTable = new DataTable();
                        adapter.Fill(storehouseTable);
                    }

                   

                    dataGridView1.DataSource = storehouseTable;
                    GridStyle.Apply(dataGridView1);
                    dataGridView1.Columns["idСклада"].Visible = false;
                    ApplyAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyAll();
        }
        private void nameFirmFiltr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyAll();
        }

        private void LoadSuppliers()
        {
            ClassConnection connection = new ClassConnection();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection.ConnectString))
                {
                    con.Open();

                    nameFirmFiltr.Items.Clear();
                    nameFirmFiltr.Items.Add("Все поставщики");
                    string query = @"
                                    SELECT SupplierCompany
                                    FROM suppliers
                                    ORDER BY SupplierCompany";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nameFirmFiltr.Items.Add(
                                reader["SupplierCompany"].ToString()
                            );
                        }
                    }

                    nameFirmFiltr.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки поставщиков: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApplyAll()
        {
            if (storehouseTable == null)
                return;

            DataView view = storehouseTable.DefaultView;

            List<string> filters = new List<string>();

            // ПОИСК ПО НАЗВАНИЮ ТОВАРА
            string search = txtSearch.Text.Trim();

            if (search == "Поиск")
                search = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace("'", "''");

                filters.Add(
                    $"[Название товара] LIKE '%{search}%'"
                );
            }


            // ФИЛЬТР ПО ПОСТАВЩИКУ
            string supplier = nameFirmFiltr.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(supplier) &&
                supplier != "Все поставщики")
            {
                supplier = supplier.Replace("'", "''");

                filters.Add(
                    $"[Поставщик] = '{supplier}'"
                );
            }

            // ПРИМЕНЕНИЕ ФИЛЬТРОВ

            if (filters.Count > 0)
            {
                view.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                view.RowFilter = "";
            }

            dataGridView1.DataSource = view;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string documentsPath =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments);

            string excelFilePath =
                Path.Combine(documentsPath, "storehouse.xlsx");

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet sheet = null;

            try
            {
                if (File.Exists(excelFilePath))
                {
                    File.Delete(excelFilePath);
                }

                application = new Excel.Application();
                application.Visible = true;
                application.DisplayAlerts = false;

                workbook = application.Workbooks.Add();
                sheet = (Excel.Worksheet)workbook.ActiveSheet;

                sheet.Name = "storehouse";

                sheet.Cells[1, 1] = "idСклада";
                sheet.Cells[1, 2] = "Количество на складе";
                sheet.Cells[1, 3] = "Количество отгруженных товаров";
                sheet.Cells[1, 4] = "Дата поставки";
                sheet.Cells[1, 5] = "Название товара";
                sheet.Cells[1, 6] = "Поставщик";

                int rowExcel = 2;

                if (storehouseTable != null)
                {
                    DataView view = storehouseTable.DefaultView;

                    foreach (DataRowView row in view)
                    {
                        sheet.Cells[rowExcel, 1] =
                            row["idСклада"].ToString();

                        sheet.Cells[rowExcel, 2] =
                            row["Количество на складе"].ToString();

                        sheet.Cells[rowExcel, 3] =
                            row["Количество отгруженных товаров"].ToString();

                        sheet.Cells[rowExcel, 4] =
                            row["Дата поставки"].ToString();

                        sheet.Cells[rowExcel, 5] =
                            row["Название товара"].ToString();

                        sheet.Cells[rowExcel, 6] =
                            row["Поставщик"].ToString();

                        rowExcel++;
                    }
                }

                Excel.Range header =
                    sheet.Range["A1", "F1"];

                header.Font.Bold = true;

                sheet.Columns.AutoFit();
                sheet.Rows.AutoFit();

                workbook.SaveAs(
                    excelFilePath,
                    Excel.XlFileFormat.xlOpenXMLWorkbook
                );

                application.DisplayAlerts = true;

                MessageBox.Show(
                    "Данные успешно экспортированы в Excel.\n\n" +
                    "Файл сохранен:\n" +
                    excelFilePath,
                    "Экспорт",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при экспорте: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.PaleVioletRed;
            }
        }
    }
    
}
