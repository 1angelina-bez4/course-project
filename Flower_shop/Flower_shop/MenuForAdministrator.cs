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
    public partial class MenuForAdministrator : Form
    {
        private int userId;
        public MenuForAdministrator(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void MenuForAdministrator_Load(object sender, EventArgs e)
        {
            LoadNameUsers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
             new UserForm().ShowDialog();
            this.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadNameUsers()
        {
            UserInfoHelper.SetUserLabel(VisualUser, userId, "Администратор");
        }
    }
}
