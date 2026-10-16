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
    public partial class warehouse : Form
    {
        public warehouse()
        {
            InitializeComponent();
        }

        private void warehouse_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            ClassConnection connect = new ClassConnection();
            using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
            {
                con.Open();
                string command = @"SELECT 
                                        s. QuantityInStock As 'Количество на складе',
                                        s. NumberOfShippedItems As 'Количество поставленных товаров',
                                        s. DeliveryDate AS 'Дата поставки',
                                        p. Name As 'Наименование товара',
                                        sup. SupplierCompany As'Наименование фирмы'
                                FROM storehouse s
                                inner join product p On  p.idProduct = s.Product_idProduct
                                inner join suppliers sup On  sup.idSuppliers  = s.Suppliers_idSuppliers;";

                MySqlDataAdapter ad = new MySqlDataAdapter(command, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dataGridView1.DataSource = dt;
                GridStyle.Apply(dataGridView1);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
