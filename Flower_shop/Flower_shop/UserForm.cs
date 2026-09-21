using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Flower_shop
{
    public partial class UserForm : Form
    {
        private DataTable usersTable;

        public UserForm()
        {
            InitializeComponent();
        }

        private void InputLogin_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            //Одна строка — все placeholder
            TextBoxHelper.SetPlaceholders(
                (txtSearch, "Поиск"),
                (txtSurname, "Фамилия"),
                (txtName, "Имя"),
                (txtPatronymic, "Отчество"),
                (txtLogin, "Логин"),
                (txtPassword, "Пароль")
            );

            ComboBoxStyleHelper.Apply(cbSort, cbFilt, cbRole);

            LoadData();
        }

        private void LoadData()
        {
            ClassConnection classConnection = new ClassConnection();
            try
            { 
                using (MySqlConnection con = new MySqlConnection(classConnection.ConnectString))
                {
                    con.Open();

                    string Select = $@"Select u.Surname AS 'Фамилия',
	                                   u.Name AS 'Имя',
	                                   u.Patronymic AS 'Отчество',
	                                   u.PhoneNumber AS 'Номер Телефона',
	                                   u.Login AS 'Логин',
	                                   u.Password  AS 'Пароль',
	                                   r.Name AS 'Роль'
	                                   FROM user u
                                       JOIN role r ON r.idRole = u.idRole;";

                    using (MySqlDataAdapter ad = new MySqlDataAdapter(Select, con))
                    {
                        usersTable = new DataTable();
                        ad.Fill(usersTable);
                    }

                    dataGridView1.DataSource = usersTable;
                    GridStyle.Apply(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Поиск";
            txtSearch.ForeColor = Color.Gray;

            if (cbRole.Items.Count > 0) cbRole.SelectedIndex = 0;
            if (cbFilt.Items.Count > 0) cbFilt.SelectedIndex = 0;
            if (cbSort.Items.Count > 0) cbSort.SelectedIndex = 0;

            if (usersTable != null)
            {
                usersTable.DefaultView.RowFilter = "";
                usersTable.DefaultView.Sort = "";
            }

            dataGridView1.DataSource = usersTable;
        }
    }
}
