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
    public partial class BLE : Form
    {
        public BLE()
        {
            InitializeComponent();
        }

        private void BLE_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(200);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BLE:LOOPBACK_START\r\n");
            Thread.Sleep(100);
            PT601_TEST.pCurrentWin.serialPort1.Write("$BLE:SEND_DATA:LOOPBACK_successful.\r\n");
        }

        public bool isClick_ble = false;
        private void button2_Click(object sender, EventArgs e)
        {
            isClick_ble = false;
            //PT601_TEST.pCurrentWin.serialPort1.Write("$BLE:STOP\r\n");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClick_ble = true;
            //PT601_TEST.pCurrentWin.serialPort1.Write("$BLE:STOP\r\n");
            this.Close();
        }
    }
}
