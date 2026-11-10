using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Flower_shop
{
    public partial class SalesForm : Form
    {
        private DataTable salesTable;
        private string _currentUser;

        private readonly string selectCmd = @"
                SELECT 
                    CAST(s.idSale AS CHAR) AS 'ID', 
                    s.SaleDate AS 'Дата',
                    s.TotalAmount AS 'Сумма',
                    s.PaymentType AS 'Тип оплаты',
                    CONCAT_WS(' ', u.Surname, u.Name, u.Patronymic) AS 'ФИО пользователя'
                FROM sale s
                JOIN `user` u ON s.user_idUser = u.idUser
                ORDER BY s.SaleDate DESC;";

        //Добавлен параметр по умолчанию, чтобы работало и new SalesForm(), и new SalesForm("Имя")
        public SalesForm(/*string currentUser = "Пользователь"*/)
        {
            InitializeComponent();
            //_currentUser = currentUser;

            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbFilt.SelectedIndexChanged += cmbFilt_SelectedIndexChanged;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;

            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            txtSearch.Click += txtSearch_Click;

            btnSbros.Click += btnSbros_Click;
            btnExcel.Click += btnExcel_Click;
        }

        private void SalesForm_Load_1(object sender, EventArgs e)
        {
            // 🆕 Раскомментировано и работает: отображение текущего пользователя
            //VisualUser.Text = $"Пользователь: {_currentUser}";
            //VisualUser.ForeColor = Color.DarkGreen;
            //VisualUser.Font = new Font(VisualUser.Font, FontStyle.Bold);

            // Настройка плейсхолдера поиска (теперь ищем по ID)
            txtSearch.Text = "Поиск по ID";
            txtSearch.ForeColor = Color.PaleVioletRed;

            ComboBoxStyleHelper.Apply(cmbFilt, cmbSort);
            LoadPaymentTypes(); // 🆕 Переименовано, так как фильтруем по типу оплаты, а не статусу
            LoadSortOptions();

            LoadSalesData();
        }

        private void LoadSalesData()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(new ClassConnection().ConnectString))
                {
                    con.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd, con))
                    {
                        salesTable = new DataTable();
                        adapter.Fill(salesTable);
                    }

                    dataGridView1.DataSource = salesTable;
                    GridStyle.Apply(dataGridView1);

                    if (dataGridView1.Columns["ID"] != null)
                        dataGridView1.Columns["ID"].Visible = false;

                    ApplyAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Загрузка типов оплаты (Cash / Card), а не статусов заказа
        private void LoadPaymentTypes()
        {
            cmbFilt.Items.Clear();
            cmbFilt.Items.Add("Все типы");
            cmbFilt.Items.Add("Наличные");
            cmbFilt.Items.Add("Карта");
            cmbFilt.SelectedIndex = 0;
        }

        private void LoadSortOptions()
        {
            cmbSort.Items.Clear();
            cmbSort.Items.Add("По умолчанию (по дате)");
            cmbSort.Items.Add("По сумме (возрастание)"); // "По сумме" вместо "По цене"
            cmbSort.Items.Add("По сумме (убывание)");
            cmbSort.SelectedIndex = 0;
        }

        private void ApplyAll()
        {
            if (salesTable == null) return;

            DataView view = salesTable.DefaultView;
            List<string> filters = new List<string>();

            // ПОИСК по ID (так как в SQL это AS 'ID')
            string search = txtSearch.Text.Trim();
            if (search == "Поиск по ID") search = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace("'", "''");
                filters.Add($"[ID] LIKE '%{search}%'");
            }

            // ФИЛЬТРАЦИЯ по типу оплаты
            string paymentType = cmbFilt.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(paymentType) && paymentType != "Все типы")
            {
                // Добавляем .Trim(), чтобы убрать случайные пробелы в конце слова из базы
                paymentType = paymentType.Trim().Replace("'", "''");
                filters.Add($"[Тип оплаты] = '{paymentType}'");
            }

            // ПРИМЕНЕНИЕ ФИЛЬТРОВ
            view.RowFilter = filters.Count > 0 ? string.Join(" AND ", filters) : "";

            // СОРТИРОВКА по сумме (так как в SQL это AS 'Сумма')
            if (cmbSort.SelectedIndex == 1)
                view.Sort = "[Сумма] ASC";
            else if (cmbSort.SelectedIndex == 2)
                view.Sort = "[Сумма] DESC";
            else
                view.Sort = "";

            dataGridView1.DataSource = view;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyAll();
        private void cmbFilt_SelectedIndexChanged(object sender, EventArgs e) => ApplyAll();
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e) => ApplyAll();

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск по ID")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Поиск по ID";
                txtSearch.ForeColor = Color.PaleVioletRed;
                ApplyAll();
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск по ID")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void btnSbros_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск по ID";
            txtSearch.ForeColor = Color.PaleVioletRed;

            cmbFilt.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;

            if (salesTable != null)
            {
                salesTable.DefaultView.RowFilter = "";
                salesTable.DefaultView.Sort = "";
                dataGridView1.DataSource = salesTable;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (salesTable == null || salesTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string excelFilePath = Path.Combine(documentsPath, $"Продажи_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet sheet = null;

            try
            {
                if (File.Exists(excelFilePath)) File.Delete(excelFilePath);

                application = new Excel.Application { Visible = true, DisplayAlerts = false };
                workbook = application.Workbooks.Add();
                sheet = (Excel.Worksheet)workbook.ActiveSheet;
                sheet.Name = "Продажи";

                //Заголовки строго соответствуют именам колонок из SQL-запроса (AS '...')
                sheet.Cells[1, 1] = "ID";
                sheet.Cells[1, 2] = "Дата";
                sheet.Cells[1, 3] = "Сумма";
                sheet.Cells[1, 4] = "Тип оплаты";
                sheet.Cells[1, 5] = "ФИО пользователя";

                int rowExcel = 2;
                DataView view = salesTable.DefaultView;

                foreach (DataRowView row in view)
                {
                    sheet.Cells[rowExcel, 1] = row["ID"].ToString();
                    sheet.Cells[rowExcel, 2] = Convert.ToDateTime(row["Дата"]).ToString("dd.MM.yyyy HH:mm");
                    sheet.Cells[rowExcel, 3] = Convert.ToDecimal(row["Сумма"]).ToString("0.00");
                    sheet.Cells[rowExcel, 4] = row["Тип оплаты"].ToString();
                    sheet.Cells[rowExcel, 5] = row["ФИО пользователя"].ToString(); // Выводим ФИО вместо ID
                    rowExcel++;
                }

                // Диапазон расширен до 5 колонок (A-E)
                Excel.Range header = sheet.Range["A1", "E1"];
                header.Font.Bold = true;
                header.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.PaleVioletRed);

                sheet.Columns.AutoFit();
                sheet.Rows.AutoFit();

                workbook.SaveAs(excelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook);
                application.DisplayAlerts = true;

                MessageBox.Show($"Данные успешно экспортированы!\n\nФайл сохранен:\n{excelFilePath}",
                    "Экспорт в Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте в Excel: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (application != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}