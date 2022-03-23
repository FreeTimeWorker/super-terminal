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

namespace SuperTerminal.Manager
{
    public partial class Main : UIForm
    {
        private readonly Login _login;
        public Main(Login login)
        {
            InitializeComponent();
            _login = login;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (_login.ShowDialog() != DialogResult.OK)
            { 
                Application.Exit();
            }
        }
    }
}
