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
    public partial class BAT : Form
    {
        public BAT()
        {
            InitializeComponent();
        }

        public bool isClick_bat = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isClick_bat = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick_bat = true;
            this.Close();
        }

        private void BAT_Load(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BAT:ALL\r\n");
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BAT:ADC1\r\n");
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BAT:ADC2\r\n");
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BAT:CARBAT\r\n");
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BAT:VBAT\r\n");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
