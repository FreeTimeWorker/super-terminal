namespace SuperTerminal.Manager
{
    partial class Setting
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
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtAddress = new Sunny.UI.UITextBox();
            this.btnSave = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel1.Location = new System.Drawing.Point(57, 79);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(140, 39);
            this.uiLabel1.TabIndex = 3;
            this.uiLabel1.Text = "服务器地址：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAddress
            // 
            this.txtAddress.ButtonSymbol = 61761;
            this.txtAddress.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtAddress.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAddress.Location = new System.Drawing.Point(204, 79);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAddress.Maximum = 2147483647D;
            this.txtAddress.Minimum = -2147483648D;
            this.txtAddress.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Radius = 10;
            this.txtAddress.Size = new System.Drawing.Size(455, 39);
            this.txtAddress.Symbol = 363041;
            this.txtAddress.TabIndex = 2;
            this.txtAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAddress.Watermark = "http://localhost:8008";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSave.Location = new System.Drawing.Point(279, 162);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 242);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.txtAddress);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtAddress;
        private Sunny.UI.UIButton btnSave;
    }
}