using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    public partial class Guide : Form
    {
        private DataTable suppliersTable;
        public Guide()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"SELECT 
		                        Address As 'Адрес',
                                ContactPerson As 'Контактное лицо',
                                NumberPhone As 'Номер телефона',
                                Email As 'Почта',
                                SupplierCompany As 'Наименование фирмы'
                                From suppliers;";

                using(MySqlDataAdapter ad = new MySqlDataAdapter(cmd, con))
                {
                    suppliersTable = new DataTable();
                    ad.Fill(suppliersTable);

                }
                dataGridView1.DataSource = suppliersTable;
                GridStyle.Apply(dataGridView1);
            }
        }
        void LoadData2()
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string cmd = @"SELECT 
		                            Name As 'Наименование',
                                    Description As 'Описание'
                                    FROM categories;";

                using (MySqlDataAdapter ad = new MySqlDataAdapter(cmd, con))
                {
                    suppliersTable = new DataTable();
                    ad.Fill(suppliersTable);

                }
                dataGridView1.DataSource = suppliersTable;
                GridStyle.Apply(dataGridView1);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            LoadData2();
        }
    }
}
