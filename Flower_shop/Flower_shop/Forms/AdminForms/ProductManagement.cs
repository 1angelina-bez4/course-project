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
    public partial class ProductManagement : Form
    {
        private DataTable productTable;
        public ProductManagement()
        {
            InitializeComponent();
        }
        
        void DataLoad()
        {
            try
            {
                ClassConnection connect = new ClassConnection();
                using (MySqlConnection con = new MySqlConnection(connect.ConnectString))
                {
                    string select_cmd = @"SELECT 
		                        p. Name As 'Наименование',
                                p. Price As 'Цена',
                                c. Name As 'Наименование категории',
                                p. Description As 'Описание товара',
                                p. Image As 'Изображение'
	                        FROM product p 
                            inner join categories c on p.idCategory = c.idCategories;";

                    using (MySqlDataAdapter ad = new MySqlDataAdapter(select_cmd, con))
                    {

                        productTable = new DataTable();
                        ad.Fill(productTable);
                    }
                    dataGridView1.DataSource = productTable;
                    GridStyle.Apply(dataGridView1);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductManagement_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void InputLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void search_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
