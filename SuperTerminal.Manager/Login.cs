using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using Sunny.UI;
using System.Threading.Tasks;
using System;
using SuperTerminal.Utity;
using SuperTerminal.Model;
using SuperTerminal.Model.User;

namespace SuperTerminal.Manager
{
    public partial class Login : UIForm
    {
        private readonly Regist _regist;
        private readonly Setting _setting;
        private readonly IConfiguration _configuration;
        private readonly Common _common;
        private readonly IApiHelper _apiHelper;
        public Login(Regist regist ,Setting setting,IConfiguration configuration,Common common, IApiHelper apiHelper)
        {
            InitializeComponent();
            _regist = regist;
            _setting = setting;
            _configuration = configuration;
            _common = common;
            _apiHelper = apiHelper;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            var rsa = _common.GetRSA();
            if (rsa == null)
            {
                ShowErrorDialog("请安装证书");
                Application.Exit();
            }
            if (string.IsNullOrEmpty(_configuration["Address"]))
            {
                _setting.ShowDialog();
            }
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
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    ShowErrorTip("请输入用户名");
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    ShowErrorTip("请输入密码");
                    return;
                }
                var rsa = _common.GetRSA();
                var userName = txtUserName.Text.Trim().RSAEncrypt(rsa);
                var password = txtPassword.Text.Trim().MD5().RSAEncrypt(rsa);
                this.btnLogin.Invoke(new Action(() =>
                {
                    this.Enabled = false;
                }));
                var result = _apiHelper.Post<BoolModel<string>>("/Auth/GetToken", new ViewUserLogin { UserName = userName, Password = password,IsManager=true });
                if (result == null)
                {
                    ShowErrorTip("通信失败，请检查配置");
                    this.btnRegist.Invoke(new Action(() =>
                    {
                        this.Enabled = true;
                    }));
                    return;
                }
                if (result.Successed)
                {
                    ApiHelper.UserName = userName;
                    ApiHelper.PassWord = password;
                    ApiHelper.Token = result.Data;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    ShowErrorTip(result.Message, 5000, false);
                }
                this.btnRegist.Invoke(new Action(() =>
                {
                    this.Enabled = true;
                }));
            });
        }
    }
}
