using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SuperTerminal.Utity;
using Microsoft.Extensions.Configuration;

namespace SuperTerminal.Manager
{
    public partial class Setting : UIForm
    {
        IConfiguration _configuration;
        public Setting(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration= configuration; 
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            this.txtAddress.Text = _configuration["Address"];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            var model = new {Address=txtAddress.Text };
            string json = model.ToJson();
            using (FileStream fs = new FileStream("appsettings.json",FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(json);
                }
            }
            (_configuration as IConfigurationRoot).Reload();
            ShowSuccessNotifier("设置成功");
            this.Close();
        }
    }
}
