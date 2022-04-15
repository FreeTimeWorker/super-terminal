using Sunny.UI;
using SuperTerminal.Model.InstantMessage;
using SuperTerminal.Model.User;
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
    public partial class Main : UIForm
    {
        private readonly Login _login;
        private readonly SignalRClient _signalRClient;
        private readonly IApiHelper _apiHelper;
        private int UserId;
        public Main(Login login, IApiHelper apiHelper,SignalRClient signalRClient)
        {
            AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
            _login = login;
            _signalRClient = signalRClient;
            _apiHelper = apiHelper;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (_login.ShowDialog() != DialogResult.OK)
            { 
                Application.Exit();
            }
            Task.Factory.StartNew(() =>
            {
                _signalRClient.StartConnection();
                loadEqipment("");
                RegistReceiveMessage();
                GetUserId();
            });
        }
        private void RegistReceiveMessage()
        {
            _signalRClient.AddReceiveHandler<NoticeMessage>("ReceiveNotice", o =>
            {
                bottom.Invoke(new Action(() =>
                {
                    var control = bottom.Controls.Find($"r_{o.Sender}", false).First() as CmdResult;
                    if (control != null)
                    {
                        control.Content.AppendText($"{Environment.NewLine}{o.Content}");//{o.SenderName}:
                        control.Content.Select(control.Content.Text.Length, 0);
                        control.Content.ScrollToCaret();
                    }
                }));
            });
        }
        private void GetUserId()
        {
            UserId= _apiHelper.Get<int>($"/Auth/GetUserId");
        }
        /// <summary>
        /// 加载设备
        /// </summary>
        /// <param name="keyword"></param>
        private void loadEqipment(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) keyword = "";
            var models = _apiHelper.Get<List<ViewEquipmentModel>>($"/Equipment/GetClients?keyword={keyword}");
            TreeNode[] treeNodes = new TreeNode[models.Count];
            for (var i=0;i<models.Count;i++)
            {
                var node = new TreeNode($"{models[i].OSPlatform.ToString()}|{models[i].NickName}|{models[i].PubIp}");
                node.ForeColor = models[i].OnLine ? Color.Green : Color.Black;
                node.Tag = models[i];
                treeNodes[i] = node;
            }
            equipmentData.Invoke(new Action(() =>
            {
                equipmentData.Nodes.Clear();
                equipmentData.Nodes.AddRange(treeNodes);
            }));
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtkeyword_ButtonClick(object sender, EventArgs e)
        {
            loadEqipment(txtkeyword.Text);
        }
        private bool lstclientselected = false;
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode item in equipmentData.Nodes)
            {
                item.Checked = !lstclientselected;
            }
            lstclientselected = !lstclientselected;
        }

        private List<CmdResult> cmdResult = new List<CmdResult>();
        /// <summary>
        /// 选择后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equipmentData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool showbtnStart = false;
            foreach (TreeNode item in equipmentData.Nodes)
            {
                var itemData = (item.Tag as ViewEquipmentModel);
                if (item.Checked)
                {
                    showbtnStart = true;
                    if (cmdResult.FirstOrDefault(o => o.Name == $"r_{itemData.Id}") == null)
                    {
                        cmdResult.Add(new CmdResult()
                        {
                            Name = $"r_{itemData.Id}",
                            Title = $"{itemData.NickName}|{itemData.PubIp}|{itemData.PrivIp}",
                            Tag = item.Tag
                        });
                    }
                }
                else
                {
                    if (cmdResult.FirstOrDefault(o => o.Name == $"r_{itemData.Id}") != null)
                    {
                        cmdResult.RemoveAll(o => o.Name == $"r_{itemData.Id}");
                    }
                }
            }
            btnStart.Visible = showbtnStart;
            btnEnd.Visible = showbtnStart;
            if (showbtnStart)
            {
                bottom.Controls.Clear();
                ControlToControlResize(cmdResult.ToArray(), this.bottom, new Padding());
            }
            else
            {
                bottom.Controls.Clear();
            }
        }
        private void ControlToControlResize(Control[] ControlArry, Control control_parent, Padding pad)
        {
            //列数
            int yCount = 1; int xCount = 0;//一行最多显示三个
            if (ControlArry.Length <= 3) //定义一列展示的数量大于总控件
            {
                yCount = 1;
                xCount = ControlArry.Length;
            }
            else
            {
                xCount = 3;
                yCount = ControlArry.Length % 3 == 0 ? ControlArry.Length / 3 : ControlArry.Length / 3 + 1;
            }
            Padding ParentsPadding = control_parent.Padding;
            Size btnSize = new();
            btnSize.Width = Convert.ToInt32(Math.Floor(((double)control_parent.Width - (ParentsPadding.Left + ParentsPadding.Right)) / xCount));
            btnSize.Height = Convert.ToInt32(Math.Floor(((double)control_parent.Height - (ParentsPadding.Top + ParentsPadding.Bottom)) / yCount));
            int index = 0;
            for (int i = 0; i < yCount; i++)//行数
            {
                for (int j = 0; j < xCount; j++)//一行多少个
                {
                    if (index >= ControlArry.Length)
                    {
                        break;
                    }
                    else
                    {
                        ControlArry[index].Size = btnSize;
                        ControlArry[index].Padding = pad;
                        ControlArry[index].Location = new Point(j * btnSize.Width + ParentsPadding.Left, i * btnSize.Height + ParentsPadding.Top);
                        index++;
                    }
                }
            }
            control_parent.Controls.AddRange(ControlArry);
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var item in cmdResult)
                {
                    _signalRClient.SendMsg<OpenTerminalMessage>("SendOpenTerminal", new OpenTerminalMessage()
                    {
                        NeedReply = true,
                        Content = "",
                        Sender = UserId,
                        Receiver = (item.Tag as ViewEquipmentModel).Id
                    });
                }
            });
        }
        /// <summary>
        /// 结束控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var item in cmdResult)
                {
                    _signalRClient.SendMsg<CloseTerminalMessage>("SendCloseTerminal", new CloseTerminalMessage()
                    {
                        NeedReply = true,
                        Content = "",
                        Sender = UserId,
                        Receiver = (item.Tag as ViewEquipmentModel).Id
                    });
                }
            });
        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var commands = this.txtCmd.Text.Trim().Split('\n');
                Task.Factory.StartNew(() =>
                {
                    foreach (var item in cmdResult)
                    {
                        var cmd = commands.Last();
                        _signalRClient.SendMsg("SendExecTerminalCmd", new ExecuteTerminalCommandMessage()
                        {
                            NeedReply = true,
                            Content = cmd,
                            Sender = UserId,
                            Receiver = (item.Tag as ViewEquipmentModel).Id
                        });
                    }
                });
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (cmdResult.Count > 0)
            {
                bottom.Controls.Clear();
                ControlToControlResize(cmdResult.ToArray(), this.bottom, new Padding());
            }
        }
    }
}
