using Sunny.UI;
using SuperTerminal.Model;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTerminal.Manager
{
    public partial class Regist : UIForm
    {
        private readonly Common _common;
        private readonly IApiHelper _apiHelper;
        public Regist(Common common,IApiHelper apiHelper)
        {
            InitializeComponent();
            _common = common;
            _apiHelper = apiHelper;
        }
        private void btnRegist_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var rsa = _common.GetRSA();
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
                if (txtPassword.Text != txtRepeatPass.Text)
                {
                    ShowErrorTip("两次密码输入不一致");
                    return;
                }
                var userName = txtUserName.Text.Trim().RSAEncrypt(rsa);
                var password = txtPassword.Text.Trim().RSAEncrypt(rsa);
                this.btnRegist.Invoke(new Action(() =>
                {
                    this.btnRegist.Enabled = false;
                }));
                var result = _apiHelper.Post<BoolModel>("/Auth/RegistManager", new ViewManagerModel { UserName = userName, Password = password });
                if (result == null)
                {
                    ShowErrorTip("通信失败，请检查配置");
                    this.btnRegist.Invoke(new Action(() =>
                    {
                        this.btnRegist.Enabled = true;
                    }));
                    return;
                }
                if (result.Successed)
                {
                    ShowSuccessDialog("注册成功");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    ShowErrorTip(result.Message,5000,false);
                }
                this.btnRegist.Invoke(new Action(() =>
                {
                    this.btnRegist.Enabled = true;
                }));
            });
        }
    }
}
