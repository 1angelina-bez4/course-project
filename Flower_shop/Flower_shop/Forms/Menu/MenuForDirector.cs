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
    public partial class MenuForDirector : Form
    {
        private int userId;
        public MenuForDirector(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Auth().ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new WarehouseInspection().ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ProductViewing().ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SupplierSearch().ShowDialog();
            this.Show();
        }

        private void MenuForDirector_Load(object sender, EventArgs e)
        {
            LoadNameUsers();
        }

        private void LoadNameUsers()
        {
            UserInfoHelper.SetUserLabel(VisualUser, userId, "Директор");
        }
    }
}
