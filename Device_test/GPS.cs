using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Device_test
{
    public partial class GPS : Form
    {
        public GPS()
        {
            InitializeComponent();
        }

        private void GPS_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$GPS:START\r\n");
        }

        public bool isClick_gps = false;
        private void button1_Click(object sender, EventArgs e)
        {
            isClick_gps = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isClick_gps = false;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
