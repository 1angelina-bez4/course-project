
namespace Flower_shop
{
    partial class Auth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auth));
            this.authorization = new System.Windows.Forms.Label();
            this.Log_In = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.InputLogin = new System.Windows.Forms.TextBox();
            this.InputPasswd = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.captcha = new System.Windows.Forms.Label();
            this.InputCaptcha = new System.Windows.Forms.TextBox();
            this.timeText = new System.Windows.Forms.Label();
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
            this.authorization.Location = new System.Drawing.Point(168, 9);
            this.authorization.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.authorization.Name = "authorization";
            this.authorization.Size = new System.Drawing.Size(203, 41);
            this.authorization.TabIndex = 0;
            this.authorization.Text = "Авторизация";
            this.authorization.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Log_In
            // 
            this.Log_In.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Log_In.BackColor = System.Drawing.Color.LavenderBlush;
            this.Log_In.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Log_In.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Log_In.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Log_In.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.Log_In.Location = new System.Drawing.Point(113, 369);
            this.Log_In.Margin = new System.Windows.Forms.Padding(4);
            this.Log_In.Name = "Log_In";
            this.Log_In.Size = new System.Drawing.Size(296, 79);
            this.Log_In.TabIndex = 1;
            this.Log_In.Text = "Войти";
            this.Log_In.UseVisualStyleBackColor = false;
            this.Log_In.Click += new System.EventHandler(this.Log_In_Click);
            // 
            // login
            // 
            this.login.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.login.AutoSize = true;
            this.login.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login.ForeColor = System.Drawing.Color.Crimson;
            this.login.Location = new System.Drawing.Point(13, 90);
            this.login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(112, 41);
            this.login.TabIndex = 2;
            this.login.Text = "Логин:";
            this.login.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // password
            // 
            this.password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.password.AutoSize = true;
            this.password.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password.ForeColor = System.Drawing.Color.Crimson;
            this.password.Location = new System.Drawing.Point(3, 172);
            this.password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(128, 41);
            this.password.TabIndex = 3;
            this.password.Text = "Пароль:";
            this.password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exit
            // 
            this.exit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.exit.BackColor = System.Drawing.Color.LavenderBlush;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exit.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.exit.Location = new System.Drawing.Point(113, 466);
            this.exit.Margin = new System.Windows.Forms.Padding(4);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(296, 79);
            this.exit.TabIndex = 4;
            this.exit.Text = "Выйти";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // InputLogin
            // 
            this.InputLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.InputLogin.BackColor = System.Drawing.Color.LavenderBlush;
            this.InputLogin.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.InputLogin.ForeColor = System.Drawing.Color.Crimson;
            this.InputLogin.Location = new System.Drawing.Point(139, 87);
            this.InputLogin.Margin = new System.Windows.Forms.Padding(4);
            this.InputLogin.MaxLength = 100;
            this.InputLogin.Name = "InputLogin";
            this.InputLogin.Size = new System.Drawing.Size(251, 45);
            this.InputLogin.TabIndex = 7;
            // 
            // InputPasswd
            // 
            this.InputPasswd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.InputPasswd.BackColor = System.Drawing.Color.LavenderBlush;
            this.InputPasswd.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.InputPasswd.ForeColor = System.Drawing.Color.Crimson;
            this.InputPasswd.Location = new System.Drawing.Point(139, 169);
            this.InputPasswd.Margin = new System.Windows.Forms.Padding(4);
            this.InputPasswd.MaxLength = 25;
            this.InputPasswd.Name = "InputPasswd";
            this.InputPasswd.PasswordChar = '*';
            this.InputPasswd.Size = new System.Drawing.Size(251, 45);
            this.InputPasswd.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Flower_shop.Properties.Resources._346167;
            this.pictureBox1.Location = new System.Drawing.Point(384, 213);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 193);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Flower_shop.Properties.Resources._346167;
            this.pictureBox2.Location = new System.Drawing.Point(-65, 213);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 193);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // captcha
            // 
            this.captcha.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.captcha.AutoSize = true;
            this.captcha.BackColor = System.Drawing.Color.Transparent;
            this.captcha.Font = new System.Drawing.Font("Monotype Corsiva", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.captcha.ForeColor = System.Drawing.Color.Crimson;
            this.captcha.Location = new System.Drawing.Point(200, 257);
            this.captcha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.captcha.Name = "captcha";
            this.captcha.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.captcha.Size = new System.Drawing.Size(118, 41);
            this.captcha.TabIndex = 5;
            this.captcha.Text = "captсha";
            this.captcha.Visible = false;
            // 
            // InputCaptcha
            // 
            this.InputCaptcha.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.InputCaptcha.BackColor = System.Drawing.Color.LavenderBlush;
            this.InputCaptcha.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.InputCaptcha.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.InputCaptcha.Location = new System.Drawing.Point(164, 302);
            this.InputCaptcha.Margin = new System.Windows.Forms.Padding(4);
            this.InputCaptcha.Name = "InputCaptcha";
            this.InputCaptcha.Size = new System.Drawing.Size(187, 45);
            this.InputCaptcha.TabIndex = 6;
            this.InputCaptcha.Visible = false;
            // 
            // timeText
            // 
            this.timeText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timeText.AutoSize = true;
            this.timeText.BackColor = System.Drawing.Color.Transparent;
            this.timeText.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.timeText.ForeColor = System.Drawing.Color.Crimson;
            this.timeText.Location = new System.Drawing.Point(125, 213);
            this.timeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeText.Name = "timeText";
            this.timeText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeText.Size = new System.Drawing.Size(0, 41);
            this.timeText.TabIndex = 11;
            this.timeText.Visible = false;
            // 
            // Auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(492, 560);
            this.Controls.Add(this.timeText);
            this.Controls.Add(this.InputPasswd);
            this.Controls.Add(this.InputLogin);
            this.Controls.Add(this.InputCaptcha);
            this.Controls.Add(this.captcha);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login);
            this.Controls.Add(this.Log_In);
            this.Controls.Add(this.authorization);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Auth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label authorization;
        private System.Windows.Forms.Button Log_In;
        private System.Windows.Forms.Label login;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.TextBox InputLogin;
        private System.Windows.Forms.TextBox InputPasswd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label captcha;
        private System.Windows.Forms.TextBox InputCaptcha;
        private System.Windows.Forms.Label timeText;
    }
}

