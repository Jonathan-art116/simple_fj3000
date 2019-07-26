using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Device_test
{
    public partial class PT601_TEST : Form
    {
        public static PT601_TEST pCurrentWin = null;
        public PT601_TEST()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            pCurrentWin  = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                comboBox1.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox1.Items.Add(sValue);
                }
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
        }

        bool isOpened = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isOpened)
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = 115200;
                try
                {
                    serialPort1.Open();     //打开串口
                    button1.Text = "Close";
                    comboBox1.Enabled = false;//关闭使能
                    isOpened = true;
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(post_DataReceived);//串口接收处理函数
                }
                catch
                {
                    MessageBox.Show("SerialPort Open Fail！");
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();     //关闭串口
                    button1.Text = "Open";
                    comboBox1.Enabled = true;//打开使能
                    isOpened = false;
                }
                catch
                {
                    MessageBox.Show("SerialPort Close Fail！");
                }
            }
        }

        
        public void post_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string num = serialPort1.ReadLine();
            if (num.IndexOf('$') == 0)//&& num.EndsWith("\r\n"))
            {
                f2.textBox1.Text += num;
                //f3.textBox1.Text += num;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public GPS f2 = new GPS();
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f2.textBox1.Text = "";
                //serialPort1.Write("$GPS:START\r\n");
                f2.ShowDialog();
                if (f2.isClick_gps == true)
                {
                    button2.ForeColor = Color.Blue;
                }
                else
                {
                    button2.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public LED f3 = new LED();
        private void button3_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f3.ShowDialog();
                if (f3.isClick_led == true)
                {
                    button3.ForeColor = Color.Blue;
                }
                else
                {
                    button3.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }
    }
}
