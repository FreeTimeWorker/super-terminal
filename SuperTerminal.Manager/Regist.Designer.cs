namespace SuperTerminal.Manager
{
    partial class Regist
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
            this.btnRegist = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtPassword = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtUserName = new Sunny.UI.UITextBox();
            this.txtRepeatPass = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // btnRegist
            // 
            this.btnRegist.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRegist.Location = new System.Drawing.Point(184, 315);
            this.btnRegist.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(100, 35);
            this.btnRegist.TabIndex = 4;
            this.btnRegist.Text = "注册";
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel2.Location = new System.Drawing.Point(39, 158);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 39);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "密码:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            this.txtPassword.ButtonSymbol = 61761;
            this.txtPassword.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.Location = new System.Drawing.Point(146, 158);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Maximum = 2147483647D;
            this.txtPassword.Minimum = -2147483648D;
            this.txtPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '⚫';
            this.txtPassword.Radius = 10;
            this.txtPassword.Size = new System.Drawing.Size(321, 39);
            this.txtPassword.Symbol = 61475;
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPassword.Watermark = "密码";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel1.Location = new System.Drawing.Point(39, 87);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 39);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "用户名：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            this.txtUserName.ButtonSymbol = 61761;
            this.txtUserName.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUserName.Location = new System.Drawing.Point(146, 87);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUserName.Maximum = 2147483647D;
            this.txtUserName.Minimum = -2147483648D;
            this.txtUserName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Radius = 10;
            this.txtUserName.Size = new System.Drawing.Size(321, 39);
            this.txtUserName.Symbol = 61447;
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUserName.Watermark = "用户名";
            // 
            // txtRepeatPass
            // 
            this.txtRepeatPass.ButtonSymbol = 61761;
            this.txtRepeatPass.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtRepeatPass.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRepeatPass.Location = new System.Drawing.Point(146, 230);
            this.txtRepeatPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRepeatPass.Maximum = 2147483647D;
            this.txtRepeatPass.Minimum = -2147483648D;
            this.txtRepeatPass.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtRepeatPass.Name = "txtRepeatPass";
            this.txtRepeatPass.PasswordChar = '⚫';
            this.txtRepeatPass.Radius = 10;
            this.txtRepeatPass.Size = new System.Drawing.Size(321, 39);
            this.txtRepeatPass.Symbol = 61475;
            this.txtRepeatPass.TabIndex = 3;
            this.txtRepeatPass.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtRepeatPass.Watermark = "确认密码";
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel3.Location = new System.Drawing.Point(39, 230);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(100, 39);
            this.uiLabel3.TabIndex = 0;
            this.uiLabel3.Text = "重复密码:";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Regist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 387);
            this.Controls.Add(this.btnRegist);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.txtRepeatPass);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.txtUserName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Regist";
            this.Text = "Regist";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIButton btnRegist;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox txtPassword;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtUserName;
        private Sunny.UI.UITextBox txtRepeatPass;
        private Sunny.UI.UILabel uiLabel3;
    }
}