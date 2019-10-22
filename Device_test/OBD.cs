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
    public partial class OBD : Form
    {
        public OBD()
        {
            InitializeComponent();
        }

        private void OBD_Load(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            PT601_TEST.pCurrentWin.serialPort1.Write("$OBD:ver\r\n");
        }

        public bool isClick_obd = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isClick_obd = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick_obd = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
