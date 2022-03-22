using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Sunny.UI;

namespace SuperTerminal.Manager
{
    public partial class Login : UIForm
    {
        private readonly Regist _regist;
        private readonly Setting _setting;
        public Login(Regist regist ,Setting setting)
        {
            InitializeComponent();
            _regist = regist;
            _setting = setting;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
        }

        private void btnSetting_Click(object sender, System.EventArgs e)
        {
            _setting.ShowDialog();
        }

        private void btnRegist_Click(object sender, System.EventArgs e)
        {
            _regist.ShowDialog();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {

        }
    }
}
