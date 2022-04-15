using System.Windows.Forms;

namespace SuperTerminal.Manager
{
    partial class Main
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
            this.left = new Sunny.UI.UIPanel();
            this.equipmentData = new TreeView();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.txtkeyword = new Sunny.UI.UITextBox();
            this.topright = new Sunny.UI.UIPanel();
            this.txtCmd = new Sunny.UI.UIRichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnd = new Sunny.UI.UIButton();
            this.btnStart = new Sunny.UI.UIButton();
            this.bottom = new System.Windows.Forms.Panel();
            this.left.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.topright.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // left
            // 
            this.left.Controls.Add(this.equipmentData);
            this.left.Controls.Add(this.uiPanel2);
            this.left.Controls.Add(this.uiPanel1);
            this.left.Dock = System.Windows.Forms.DockStyle.Left;
            this.left.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.left.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.left.Location = new System.Drawing.Point(0, 35);
            this.left.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.left.MinimumSize = new System.Drawing.Size(1, 1);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(253, 723);
            this.left.TabIndex = 0;
            this.left.Text = null;
            this.left.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // equipmentData
            // 
            this.equipmentData.CheckBoxes = true;
            this.equipmentData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipmentData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.equipmentData.Location = new System.Drawing.Point(0, 35);
            this.equipmentData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.equipmentData.MinimumSize = new System.Drawing.Size(1, 1);
            this.equipmentData.Name = "equipmentData";
            this.equipmentData.SelectedNode = null;
            this.equipmentData.Size = new System.Drawing.Size(253, 660);
            this.equipmentData.TabIndex = 3;
            this.equipmentData.Text = null;
            this.equipmentData.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.equipmentData_AfterCheck);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.checkAll);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiPanel2.Location = new System.Drawing.Point(0, 695);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(253, 28);
            this.uiPanel2.TabIndex = 4;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkAll
            // 
            this.checkAll.AutoSize = true;
            this.checkAll.Location = new System.Drawing.Point(3, 3);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(61, 25);
            this.checkAll.TabIndex = 0;
            this.checkAll.Text = "全选";
            this.checkAll.UseVisualStyleBackColor = true;
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.txtkeyword);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(253, 35);
            this.uiPanel1.TabIndex = 2;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtkeyword
            // 
            this.txtkeyword.ButtonSymbol = 61452;
            this.txtkeyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtkeyword.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txtkeyword.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtkeyword.Location = new System.Drawing.Point(0, 0);
            this.txtkeyword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtkeyword.Maximum = 2147483647D;
            this.txtkeyword.Minimum = -2147483648D;
            this.txtkeyword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtkeyword.Name = "txtkeyword";
            this.txtkeyword.ShowButton = true;
            this.txtkeyword.Size = new System.Drawing.Size(253, 35);
            this.txtkeyword.TabIndex = 0;
            this.txtkeyword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtkeyword.Watermark = "别名/IP";
            this.txtkeyword.ButtonClick += new System.EventHandler(this.txtkeyword_ButtonClick);
            // 
            // topright
            // 
            this.topright.Controls.Add(this.txtCmd);
            this.topright.Controls.Add(this.panel1);
            this.topright.Dock = System.Windows.Forms.DockStyle.Top;
            this.topright.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.topright.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.topright.Location = new System.Drawing.Point(253, 35);
            this.topright.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.topright.MinimumSize = new System.Drawing.Size(1, 1);
            this.topright.Name = "topright";
            this.topright.Size = new System.Drawing.Size(685, 92);
            this.topright.TabIndex = 1;
            this.topright.Text = null;
            this.topright.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCmd
            // 
            this.txtCmd.AutoWordSelection = true;
            this.txtCmd.BackColor = System.Drawing.Color.Black;
            this.txtCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCmd.FillColor = System.Drawing.Color.Black;
            this.txtCmd.FillColor2 = System.Drawing.Color.Black;
            this.txtCmd.FillDisableColor = System.Drawing.Color.Black;
            this.txtCmd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCmd.ForeColor = System.Drawing.Color.White;
            this.txtCmd.Location = new System.Drawing.Point(0, 0);
            this.txtCmd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCmd.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Padding = new System.Windows.Forms.Padding(2);
            this.txtCmd.Size = new System.Drawing.Size(541, 92);
            this.txtCmd.Style = Sunny.UI.UIStyle.Custom;
            this.txtCmd.StyleCustomMode = true;
            this.txtCmd.TabIndex = 0;
            this.txtCmd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtCmd.WordWrap = true;
            this.txtCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmd_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(541, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 92);
            this.panel1.TabIndex = 1;
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnEnd.Location = new System.Drawing.Point(17, 50);
            this.btnEnd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Radius = 20;
            this.btnEnd.Size = new System.Drawing.Size(100, 33);
            this.btnEnd.TabIndex = 4;
            this.btnEnd.Text = "结束控制";
            this.btnEnd.Visible = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(17, 11);
            this.btnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Radius = 20;
            this.btnStart.Size = new System.Drawing.Size(100, 33);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "开始控制";
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // bottom
            // 
            this.bottom.AutoScroll = true;
            this.bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottom.Location = new System.Drawing.Point(253, 127);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(685, 631);
            this.bottom.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 758);
            this.Controls.Add(this.bottom);
            this.Controls.Add(this.topright);
            this.Controls.Add(this.left);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.left.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel2.PerformLayout();
            this.uiPanel1.ResumeLayout(false);
            this.topright.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel left;
        private Sunny.UI.UIPanel topright;
        private Sunny.UI.UITextBox txtkeyword;
        private Sunny.UI.UIPanel uiPanel1;
        private TreeView equipmentData;
        private Sunny.UI.UIPanel uiPanel2;
        private System.Windows.Forms.CheckBox checkAll;
        private Sunny.UI.UIRichTextBox txtCmd;
        private Sunny.UI.UIButton btnStart;
        private Sunny.UI.UIButton btnEnd;
        private System.Windows.Forms.Panel bottom;
        private System.Windows.Forms.Panel panel1;
    }
}