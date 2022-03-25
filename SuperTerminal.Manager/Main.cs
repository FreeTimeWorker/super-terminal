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
                        control.Content.AppendText($"{Environment.NewLine}{o.SenderName}:{o.Content}");
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
            foreach (var item in models)
            {
                var node = new TreeNode($"{item.NickName}|{item.PubIp}");
                node.ForeColor = item.OnLine ? Color.Black : Color.Red;
                node.Tag = item;
                treeNodes[0] = node;
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
                    if (cmdResult.FirstOrDefault(o => o.Name == $"r{itemData.Id}") != null)
                    {
                        cmdResult.RemoveAll(o => o.Name == $"r_{itemData.Id}");
                    }
                }
            }
            btnStart.Visible = showbtnStart;
            btnEnd.Visible = showbtnStart;
            if (showbtnStart)
            {
                btnStart.Parent = this;
                btnEnd.Parent = this;
                btnEnd.BringToFront();
                btnStart.BringToFront();
            }
        }
        private void ControlToControlResize(Control[] ControlArry, Control control_parent, int RowCount, Size? ControlSize, Padding pad)
        {
            //计算按钮相关信息
            control_parent.Controls.Clear();
            //列数
            int yCount = 0; int xCount = RowCount;
            if (ControlArry.Length < RowCount) //定义一列展示的数量大于总控件
            {
                yCount = 1;
            }
            else
            {
                yCount = ControlArry.Length % RowCount == 0 ? ControlArry.Length / RowCount : ControlArry.Length / RowCount + 1;
            }
            Padding ParentsPadding = control_parent.Padding;
            Size btnSize = new System.Drawing.Size();
            if (ControlSize != null)
            {
                btnSize = (Size)ControlSize;
            }
            else
            {
                btnSize.Width = Convert.ToInt32(Math.Floor(((double)control_parent.Width - (ParentsPadding.Left + ParentsPadding.Right)) / RowCount));
                btnSize.Height = Convert.ToInt32(Math.Floor(((double)control_parent.Height - (ParentsPadding.Top + ParentsPadding.Bottom)) / yCount));
            }
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
            ControlToControlResize(cmdResult.ToArray(), bottom, (cmdResult.Count / 3) + 1, null, new Padding(1,1,1,1));
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
                this.txtCmd.Text = this.txtCmd.Text.Trim();
            }
        }
    }
}
