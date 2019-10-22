using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Device_test
{
    public partial class H3LIS331DL : Form
    {
        public H3LIS331DL()
        {
            InitializeComponent();
        }

        private void H3LIS331DL_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$H3L:START\r\n");
        }

        public bool isClick_h3l = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isClick_h3l = false;
            PT601_TEST.pCurrentWin.serialPort1.Write("$H3L:STOP\r\n");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick_h3l = true; ;
            PT601_TEST.pCurrentWin.serialPort1.Write("$H3L:STOP\r\n");
            this.Close();
        }
    }
}
