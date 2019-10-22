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
    public partial class LED : Form
    {
        public LED()
        {
            InitializeComponent();
        }

        public bool isClick_led = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isClick_led = false; 
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick_led = true;
            this.Close();
        }
        
        public void button3_Click(object sender, EventArgs e)
        {
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:ON:GREEN\r\n");
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:ON:RED\r\n");
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:ON:YELLOW\r\n");
        }

        public void button4_Click(object sender, EventArgs e)
        {
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:OFF:GREEN\r\n");
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:OFF:RED\r\n");
            PT601_TEST.pCurrentWin.serialPort1.Write("$LED:OFF:YELLOW\r\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void LED_Load(object sender, EventArgs e)
        {

        }
    }
}
