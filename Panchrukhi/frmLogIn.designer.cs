namespace Panchrukhi
{
    partial class frmLogIn
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
            this.lblPgrsPersent = new System.Windows.Forms.Label();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblLogInError = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblPgrsPersent
            // 
            this.lblPgrsPersent.AutoSize = true;
            this.lblPgrsPersent.ForeColor = System.Drawing.Color.White;
            this.lblPgrsPersent.Location = new System.Drawing.Point(651, 206);
            this.lblPgrsPersent.Name = "lblPgrsPersent";
            this.lblPgrsPersent.Size = new System.Drawing.Size(0, 13);
            this.lblPgrsPersent.TabIndex = 3;
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationName.ForeColor = System.Drawing.Color.White;
            this.lblApplicationName.Location = new System.Drawing.Point(341, 88);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(106, 32);
            this.lblApplicationName.TabIndex = 0;
            this.lblApplicationName.Text = "Payroll";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(208, 147);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(77, 16);
            this.lblUserName.TabIndex = 7;
            this.lblUserName.Text = "User Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(208, 177);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 16);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(293, 147);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(185, 20);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyUp);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(293, 177);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(185, 20);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyUp);
            // 
            // lblLogInError
            // 
            this.lblLogInError.AutoSize = true;
            this.lblLogInError.ForeColor = System.Drawing.Color.Maroon;
            this.lblLogInError.Location = new System.Drawing.Point(265, 244);
            this.lblLogInError.Name = "lblLogInError";
            this.lblLogInError.Size = new System.Drawing.Size(0, 13);
            this.lblLogInError.TabIndex = 11;
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.BackColor = System.Drawing.Color.DarkGray;
            this.lblClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblClose.Location = new System.Drawing.Point(701, 4);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(33, 13);
            this.lblClose.TabIndex = 12;
            this.lblClose.Text = "Close";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(416, 212);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(61, 23);
            this.btnLogin.TabIndex = 13;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblCompanyName.Location = new System.Drawing.Point(100, 20);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(0, 61);
            this.lblCompanyName.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Panchrukhi.Properties.Resources.FKLIT;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(604, 244);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 38);
            this.panel1.TabIndex = 15;
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(738, 294);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblLogInError);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblPgrsPersent);
            this.Controls.Add(this.lblApplicationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Window";
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPgrsPersent;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblLogInError;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Panel panel1;
    }
}

