using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flower_shop
{
    public partial class MenuForSalesman : Form
    {
        private int userId;
        public MenuForSalesman(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ProductViewing().ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Checkout().ShowDialog();
            this.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Auth().ShowDialog();
            this.Close();
        }

        private void MenuForSalesman_Load(object sender, EventArgs e)
        {
            LoadNameUsers();
        }
        
        private void LoadNameUsers()
        {
            UserInfoHelper.SetUserLabel(VisualUser, userId, "Продавец");
        }
    }
}
