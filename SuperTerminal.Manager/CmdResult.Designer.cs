namespace SuperTerminal.Manager
{
    partial class CmdResult
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.g_title = new System.Windows.Forms.GroupBox();
            this.Content = new Sunny.UI.UIRichTextBox();
            this.g_title.SuspendLayout();
            this.SuspendLayout();
            // 
            // g_title
            // 
            this.g_title.Controls.Add(this.Content);
            this.g_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.g_title.Location = new System.Drawing.Point(0, 0);
            this.g_title.Name = "g_title";
            this.g_title.Size = new System.Drawing.Size(389, 294);
            this.g_title.TabIndex = 0;
            this.g_title.TabStop = false;
            // 
            // Content
            // 
            this.Content.AutoWordSelection = true;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.FillColor = System.Drawing.Color.Black;
            this.Content.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Content.ForeColor = System.Drawing.Color.White;
            this.Content.Location = new System.Drawing.Point(3, 19);
            this.Content.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Content.MinimumSize = new System.Drawing.Size(1, 1);
            this.Content.Name = "Content";
            this.Content.Padding = new System.Windows.Forms.Padding(2);
            this.Content.Size = new System.Drawing.Size(383, 272);
            this.Content.Style = Sunny.UI.UIStyle.Custom;
            this.Content.TabIndex = 0;
            this.Content.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.Content.WordWrap = true;
            // 
            // CmdResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.g_title);
            this.Name = "CmdResult";
            this.Size = new System.Drawing.Size(389, 294);
            this.g_title.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g_title;
        public Sunny.UI.UIRichTextBox Content;
    }
}
