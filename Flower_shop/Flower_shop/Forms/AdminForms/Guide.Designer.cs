
namespace Flower_shop
{
    partial class Guide
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Guide));
            this.authorization = new System.Windows.Forms.Label();
            this.VisualUser = new System.Windows.Forms.Label();
            this.tabCategory = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDelSup = new System.Windows.Forms.Button();
            this.btnEdtSup = new System.Windows.Forms.Button();
            this.btnAddSup = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddCat = new System.Windows.Forms.Button();
            this.btnEdtCat = new System.Windows.Forms.Button();
            this.btnDelCat = new System.Windows.Forms.Button();
            this.txtCategoryDescription = new System.Windows.Forms.TextBox();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtSupplierCompany = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.tabCategory.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // authorization
            // 
            this.authorization.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.authorization.AutoSize = true;
            this.authorization.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.authorization.ForeColor = System.Drawing.Color.Crimson;
            this.authorization.Location = new System.Drawing.Point(375, 1);
            this.authorization.Name = "authorization";
            this.authorization.Size = new System.Drawing.Size(196, 41);
            this.authorization.TabIndex = 1;
            this.authorization.Text = "Справочники";
            this.authorization.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VisualUser
            // 
            this.VisualUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VisualUser.AutoSize = true;
            this.VisualUser.Font = new System.Drawing.Font("Monotype Corsiva", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VisualUser.ForeColor = System.Drawing.Color.Crimson;
            this.VisualUser.Location = new System.Drawing.Point(612, 9);
            this.VisualUser.Name = "VisualUser";
            this.VisualUser.Size = new System.Drawing.Size(177, 27);
            this.VisualUser.TabIndex = 9;
            this.VisualUser.Text = "Администратор:";
            // 
            // tabCategory
            // 
            this.tabCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCategory.Controls.Add(this.tabPage1);
            this.tabCategory.Controls.Add(this.tabPage2);
            this.tabCategory.Location = new System.Drawing.Point(12, 45);
            this.tabCategory.Name = "tabCategory";
            this.tabCategory.SelectedIndex = 0;
            this.tabCategory.Size = new System.Drawing.Size(984, 611);
            this.tabCategory.TabIndex = 10;
            this.tabCategory.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtPhone);
            this.tabPage1.Controls.Add(this.txtSupplierCompany);
            this.tabPage1.Controls.Add(this.btnDelSup);
            this.tabPage1.Controls.Add(this.btnEdtSup);
            this.tabPage1.Controls.Add(this.btnAddSup);
            this.tabPage1.Controls.Add(this.txtAddress);
            this.tabPage1.Controls.Add(this.txtContactPerson);
            this.tabPage1.Controls.Add(this.txtEmail);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(4, 50);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(976, 557);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Поставщики";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnDelSup
            // 
            this.btnDelSup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelSup.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnDelSup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelSup.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelSup.ForeColor = System.Drawing.Color.Crimson;
            this.btnDelSup.Location = new System.Drawing.Point(329, 497);
            this.btnDelSup.Name = "btnDelSup";
            this.btnDelSup.Size = new System.Drawing.Size(305, 57);
            this.btnDelSup.TabIndex = 32;
            this.btnDelSup.Text = "Удалить";
            this.btnDelSup.UseVisualStyleBackColor = false;
            // 
            // btnEdtSup
            // 
            this.btnEdtSup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEdtSup.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnEdtSup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdtSup.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEdtSup.ForeColor = System.Drawing.Color.Crimson;
            this.btnEdtSup.Location = new System.Drawing.Point(658, 494);
            this.btnEdtSup.Name = "btnEdtSup";
            this.btnEdtSup.Size = new System.Drawing.Size(309, 57);
            this.btnEdtSup.TabIndex = 31;
            this.btnEdtSup.Text = "Редактировать";
            this.btnEdtSup.UseVisualStyleBackColor = false;
            // 
            // btnAddSup
            // 
            this.btnAddSup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddSup.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnAddSup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddSup.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddSup.ForeColor = System.Drawing.Color.Crimson;
            this.btnAddSup.Location = new System.Drawing.Point(9, 494);
            this.btnAddSup.Name = "btnAddSup";
            this.btnAddSup.Size = new System.Drawing.Size(275, 57);
            this.btnAddSup.TabIndex = 30;
            this.btnAddSup.Text = "Добавить\r\n";
            this.btnAddSup.UseVisualStyleBackColor = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtAddress.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtAddress.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtAddress.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtAddress.Location = new System.Drawing.Point(565, 439);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(363, 52);
            this.txtAddress.TabIndex = 29;
            this.txtAddress.Text = "Адрес";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtContactPerson.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtContactPerson.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtContactPerson.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtContactPerson.Location = new System.Drawing.Point(294, 384);
            this.txtContactPerson.MaxLength = 255;
            this.txtContactPerson.Multiline = true;
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(363, 45);
            this.txtContactPerson.TabIndex = 22;
            this.txtContactPerson.Text = "Контактное лицо";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtEmail.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtEmail.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtEmail.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtEmail.Location = new System.Drawing.Point(3, 384);
            this.txtEmail.MaxLength = 255;
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(278, 45);
            this.txtEmail.TabIndex = 21;
            this.txtEmail.Text = "Email";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(964, 372);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAddCat);
            this.tabPage2.Controls.Add(this.btnEdtCat);
            this.tabPage2.Controls.Add(this.btnDelCat);
            this.tabPage2.Controls.Add(this.txtCategoryDescription);
            this.tabPage2.Controls.Add(this.txtCategoryName);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 50);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 557);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Категории";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btnAddCat
            // 
            this.btnAddCat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddCat.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnAddCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddCat.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddCat.ForeColor = System.Drawing.Color.Crimson;
            this.btnAddCat.Location = new System.Drawing.Point(6, 476);
            this.btnAddCat.Name = "btnAddCat";
            this.btnAddCat.Size = new System.Drawing.Size(318, 57);
            this.btnAddCat.TabIndex = 36;
            this.btnAddCat.Text = "Добавить";
            this.btnAddCat.UseVisualStyleBackColor = false;
            // 
            // btnEdtCat
            // 
            this.btnEdtCat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEdtCat.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnEdtCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdtCat.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEdtCat.ForeColor = System.Drawing.Color.Crimson;
            this.btnEdtCat.Location = new System.Drawing.Point(6, 413);
            this.btnEdtCat.Name = "btnEdtCat";
            this.btnEdtCat.Size = new System.Drawing.Size(318, 57);
            this.btnEdtCat.TabIndex = 35;
            this.btnEdtCat.Text = "Редактировать";
            this.btnEdtCat.UseVisualStyleBackColor = false;
            // 
            // btnDelCat
            // 
            this.btnDelCat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelCat.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnDelCat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelCat.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelCat.ForeColor = System.Drawing.Color.Crimson;
            this.btnDelCat.Location = new System.Drawing.Point(329, 476);
            this.btnDelCat.Name = "btnDelCat";
            this.btnDelCat.Size = new System.Drawing.Size(288, 57);
            this.btnDelCat.TabIndex = 34;
            this.btnDelCat.Text = "Удалить";
            this.btnDelCat.UseVisualStyleBackColor = false;
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtCategoryDescription.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtCategoryDescription.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtCategoryDescription.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtCategoryDescription.Location = new System.Drawing.Point(634, 421);
            this.txtCategoryDescription.MaxLength = 302;
            this.txtCategoryDescription.Multiline = true;
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(339, 112);
            this.txtCategoryDescription.TabIndex = 22;
            this.txtCategoryDescription.Text = "Описание";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtCategoryName.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtCategoryName.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtCategoryName.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtCategoryName.Location = new System.Drawing.Point(330, 413);
            this.txtCategoryName.MaxLength = 302;
            this.txtCategoryName.Multiline = true;
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(287, 57);
            this.txtCategoryName.TabIndex = 21;
            this.txtCategoryName.Text = "Наименование";
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 6);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(964, 389);
            this.dataGridView2.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button4.BackColor = System.Drawing.Color.LavenderBlush;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.Crimson;
            this.button4.Location = new System.Drawing.Point(345, 662);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(288, 57);
            this.button4.TabIndex = 33;
            this.button4.Text = "Выйти в меню";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Flower_shop.Properties.Resources._346167;
            this.pictureBox1.Location = new System.Drawing.Point(-27, 418);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(472, 383);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Flower_shop.Properties.Resources._346167;
            this.pictureBox2.Location = new System.Drawing.Point(544, 418);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(472, 383);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 35;
            this.pictureBox2.TabStop = false;
            // 
            // txtSupplierCompany
            // 
            this.txtSupplierCompany.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtSupplierCompany.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtSupplierCompany.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtSupplierCompany.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtSupplierCompany.Location = new System.Drawing.Point(93, 434);
            this.txtSupplierCompany.MaxLength = 255;
            this.txtSupplierCompany.Multiline = true;
            this.txtSupplierCompany.Name = "txtSupplierCompany";
            this.txtSupplierCompany.Size = new System.Drawing.Size(363, 54);
            this.txtSupplierCompany.TabIndex = 33;
            this.txtSupplierCompany.Text = "Название фирмы";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtPhone.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtPhone.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtPhone.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtPhone.Location = new System.Drawing.Point(676, 377);
            this.txtPhone.MaxLength = 255;
            this.txtPhone.Multiline = true;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(291, 52);
            this.txtPhone.TabIndex = 34;
            this.txtPhone.Text = "Номер телефона";
            // 
            // Guide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tabCategory);
            this.Controls.Add(this.VisualUser);
            this.Controls.Add(this.authorization);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.ForeColor = System.Drawing.Color.Crimson;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "Guide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочники";
            this.tabCategory.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label authorization;
        private System.Windows.Forms.Label VisualUser;
        private System.Windows.Forms.TabControl tabCategory;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnDelSup;
        private System.Windows.Forms.Button btnEdtSup;
        private System.Windows.Forms.Button btnAddSup;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnAddCat;
        private System.Windows.Forms.Button btnEdtCat;
        private System.Windows.Forms.Button btnDelCat;
        private System.Windows.Forms.TextBox txtCategoryDescription;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtSupplierCompany;
        private System.Windows.Forms.TextBox txtPhone;
    }
}