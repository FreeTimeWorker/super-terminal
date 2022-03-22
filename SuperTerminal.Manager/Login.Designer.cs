namespace SuperTerminal.Manager
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiTextBox1 = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiTextBox2 = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.btnLogin = new Sunny.UI.UIButton();
            this.btnRegist = new Sunny.UI.UIButton();
            this.btnSetting = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // uiTextBox1
            // 
            this.uiTextBox1.ButtonSymbol = 61761;
            this.uiTextBox1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiTextBox1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiTextBox1.Location = new System.Drawing.Point(184, 94);
            this.uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox1.Maximum = 2147483647D;
            this.uiTextBox1.Minimum = -2147483648D;
            this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox1.Name = "uiTextBox1";
            this.uiTextBox1.Radius = 10;
            this.uiTextBox1.Size = new System.Drawing.Size(321, 39);
            this.uiTextBox1.Symbol = 61447;
            this.uiTextBox1.TabIndex = 0;
            this.uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox1.Watermark = "用户名";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel1.Location = new System.Drawing.Point(77, 94);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 39);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "用户名：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTextBox2
            // 
            this.uiTextBox2.ButtonSymbol = 61761;
            this.uiTextBox2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiTextBox2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiTextBox2.Location = new System.Drawing.Point(184, 181);
            this.uiTextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox2.Maximum = 2147483647D;
            this.uiTextBox2.Minimum = -2147483648D;
            this.uiTextBox2.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox2.Name = "uiTextBox2";
            this.uiTextBox2.PasswordChar = '⚫';
            this.uiTextBox2.Radius = 10;
            this.uiTextBox2.Size = new System.Drawing.Size(321, 39);
            this.uiTextBox2.Symbol = 61475;
            this.uiTextBox2.TabIndex = 0;
            this.uiTextBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox2.Watermark = "密码";
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel2.Location = new System.Drawing.Point(77, 181);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 39);
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "密码:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.Location = new System.Drawing.Point(177, 284);
            this.btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegist
            // 
            this.btnRegist.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRegist.Location = new System.Drawing.Point(332, 284);
            this.btnRegist.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(100, 35);
            this.btnRegist.TabIndex = 2;
            this.btnRegist.Text = "注册";
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetting.ImageInterval = 0;
            this.btnSetting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetting.IsCircle = true;
            this.btnSetting.Location = new System.Drawing.Point(545, 352);
            this.btnSetting.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(48, 45);
            this.btnSetting.Symbol = 61459;
            this.btnSetting.SymbolOffset = new System.Drawing.Point(5, 3);
            this.btnSetting.SymbolSize = 40;
            this.btnSetting.TabIndex = 3;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 400);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnRegist);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiTextBox2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiTextBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "超级终端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox uiTextBox1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox uiTextBox2;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton btnLogin;
        private Sunny.UI.UIButton btnRegist;
        private Sunny.UI.UISymbolButton btnSetting;
    }
}

