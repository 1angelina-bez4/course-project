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
    public partial class Checkout : Form
    {
        public Checkout()
        {
            InitializeComponent();
        }



        private void Checkout_Load(object sender, EventArgs e)
        {
            ClassConnection classConnection = new ClassConnection();
            using(MySqlConnection con = new MySqlConnection(classConnection.ConnectString))
            {
                con.Open();
                string command = @"SELECT 
		                        p. Name As 'Наименование',
                                p. Price As 'Цена',
                                c. Name As 'Наименование категории',
                                p. Description As 'Описание товара',
                                p. Image As 'Изображение'
	                        FROM product p 
                            inner join categories c on p.idCategory = c.idCategories;";


                string command2 = @"SELECT 
		                        p. Name As 'Наименование',
                                p. Price As 'Цена',
                                p. Image As 'Изображение'
	                        FROM product p;";


                using (MySqlDataAdapter ad = new MySqlDataAdapter(command,con))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    dataGridView1.DataSource = dt;
                    GridStyle.Apply(dataGridView1);
                }

                using (MySqlDataAdapter ad = new MySqlDataAdapter(command2, con))
                {
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    dataGridView2.DataSource = dt;
                    GridStyle.Apply(dataGridView2);
                }
            }
        }

        private void LoadData1()
        {
            ClassConnection connection = new ClassConnection();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
