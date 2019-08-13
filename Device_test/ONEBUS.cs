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
    public partial class ONEBUS : Form
    {
        public ONEBUS()
        {
            InitializeComponent();
        }


        public bool isClick_one = false;
        private void button1_Click(object sender, EventArgs e)
        {
            isClick_one = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isClick_one = false;
            this.Close();
        }

        private void ONEBUS_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$ONE:RESET\r\n");
        }
    }
}
