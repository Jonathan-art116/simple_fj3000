using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Device_test
{
    public partial class PT601_TEST : Form
    {
        public static PT601_TEST pCurrentWin = null;
        public PT601_TEST()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            pCurrentWin = this;
        }

        class read_config
        {
            public static string tbname;

        }

        public string connectString;
        public MySqlConnection cnn;
        public void Form1_Load(object sender, EventArgs e)
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
            
            string ip = "localhost";
            string dbname = "sndb";
            string usname = "root";
            string pswd = "root";
            string pot = "3306";
            read_config.tbname = "FJ300";
            connectString = "server=" + ip + ";database=" + dbname + ";uid=" + usname + ";pwd=" + pswd + ";port=" + pot;
            cnn = new MySqlConnection(connectString);

            //连接数据库
            //connectString = "server=localhost;database=sndb;uid=root;pwd=root;port=3306";
            connectString = "server=" + ip + ";database=" + dbname + ";uid=" + usname + ";pwd=" + pswd + ";port=" + pot;            
            cnn = new MySqlConnection(connectString);
            try
            {
                cnn.Open();
                string creatdatabase = "CREATE TABLE " + read_config.tbname + "(`ID` int(11) NOT NULL AUTO_INCREMENT, `SN` char(20) NOT NULL,`GPS` char(20) DEFAULT NULL,`LED` char(20) DEFAULT NULL,`Createtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY(`ID`), UNIQUE KEY `SN` (`SN`)) ENGINE = InnoDB DEFAULT CHARSET = utf8;";
                MySqlCommand creat = new MySqlCommand(creatdatabase, cnn);
                try
                {
                    creat.ExecuteNonQuery();
                }
                catch
                {
                    //MessageBox.Show("Database Connection Successful!");
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Database Disconnection!");
                //this.Close(); //关闭主窗口
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

        //读取串口返回数据
        public void post_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string p_data = (serialPort1.ReadLine() + "\r\n");
                string[] t_data = p_data.Split(':');
                if (p_data.IndexOf("$GPS") == 0)//&& num.EndsWith("\r\n"))
                {
                    //f2.textBox1.Text = "";
                    f2.textBox1.Text += p_data.Replace("$GPS:", "");
                    //string[] t_data = p_data.Split(':');
                    //int i;
                    //for(i=1;i<10;i++)
                    //{
                    //    f2.textBox1.Text += t_data[i];
                    //}
                    //f2.textBox1.Text +=  t_data[2];
                    //f2.textBox1.Text += GPS[2]; //gps测试返回数据
                    //f3.textBox1.Text += num;
                }
                else if(p_data.IndexOf("$LIS:A") == 0)
                {
                    f4.textBox1.Text = "";
                    f4.textBox1.Text = t_data[2];
                    f4.textBox2.Text = "";
                    f4.textBox2.Text = t_data[3];
                    f4.textBox3.Text = "";
                    f4.textBox3.Text = t_data[4];
                }
                else if(p_data.IndexOf("$LIS:T") == 0)
                {
                    f4.textBox4.Text = "";
                    f4.textBox4.Text = t_data[2];
                }
                else if(p_data.IndexOf("$FAT:F") == 0)
                {
                    f5.textBox1.Text = "";
                    f5.textBox1.Text = t_data[3];
                    f5.textBox2.Text = t_data[5];
                }
                else if (p_data.IndexOf("$ONE:RESET:ONEBUS1") == 0)
                {
                    f6.textBox1.Text = "";
                    f6.textBox1.Text = t_data[3];
                }
                else if (p_data.IndexOf("$ONE:RESET:ONEBUS2") == 0)
                {
                    f6.textBox2.Text = "";
                    f6.textBox2.Text = t_data[3];
                }
                else if (p_data.IndexOf("$H3L:A") == 0)
                {
                    f7.textBox1.Text = "";
                    f7.textBox1.Text = t_data[2];
                    f7.textBox2.Text = "";
                    f7.textBox2.Text = t_data[3];
                    f7.textBox3.Text = "";
                    f7.textBox3.Text = t_data[4];
                }
                else if (p_data.IndexOf("VCM") == 0)
                {
                    f10.textBox1.Text = "";
                    f10.textBox1.Text = p_data;
                    //f10.textBox1.Text = t_data[1] + ":" + t_data[2] + ":" + t_data[3];
                }
                else if (p_data.IndexOf("$BLE:DATA_RECEIVED") == 0)
                {
                    f11.textBox1.Text = "";
                    //f11.textBox1.Text = p_data;
                    f11.textBox1.Text = t_data[2];
                }
                else if (p_data.IndexOf("$LTE:MODULE") ==0)
                {
                    f13.textBox1.Text = "";
                    f13.textBox1.Text = t_data[2];
                }
                else if (p_data.IndexOf("$LTE:SIM") ==0)
                {
                    f13.textBox2.Text = "";
                    f13.textBox2.Text = t_data[2];
                }
                else if (p_data.IndexOf("$LTE:IMEI") == 0)
                {
                    f13.textBox3.Text = "";
                    f13.textBox3.Text = t_data[2];
                }
                else if (p_data.IndexOf("$LTE:IMSI") == 0)
                {
                    f13.textBox4.Text = "";
                    f13.textBox4.Text = t_data[2];
                }
                else if (p_data.IndexOf("$LTE:ICCID") == 0)
                {
                    f13.textBox5.Text = "";
                    f13.textBox5.Text = t_data[2];
                }
                else if (p_data.IndexOf("$SYS") == 0)
                {
                    f12.textBox1.Text = "";
                    f12.textBox1.Text = t_data[1] + ":" + t_data[2];
                }
                else if (p_data.IndexOf("$BAT: Charge State") == 0)
                {
                    f9.textBox4.Text = "";
                    f9.textBox4.Text = t_data[2];
                }
                else if (p_data.IndexOf("$BAT:ADC1") == 0)
                {
                    f9.textBox1.Text = "";
                    f9.textBox1.Text = t_data[3];
                }
                else if (p_data.IndexOf("$BAT:ADC2") == 0)
                {
                    f9.textBox5.Text = "";
                    f9.textBox5.Text = t_data[3];
                }
                else if (p_data.IndexOf("$BAT:CARBAT") == 0)
                {
                    f9.textBox2.Text = "";
                    f9.textBox2.Text = t_data[3];
                }
                else if (p_data.IndexOf("$BAT:VBAT") == 0)
                {
                    f9.textBox3.Text = "";
                    f9.textBox3.Text = t_data[3];
                }
            }
            catch
            {
        
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
                f2.textBox1.Text = "";//清除textbox显示内容
                //serialPort1.Write("$GPS:START\r\n");
                f2.ShowDialog();
                if (f2.isClick_gps == true)
                {
                    button2.ForeColor = Color.Blue;
                    //button3.PerformClick();
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
            if (isOpened)// && button2.ForeColor == Color.Blue)
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
            //else if (button2.ForeColor == Color.Red)
            //{
            //    MessageBox.Show("Please complete the GPS test");
            //}
            else 
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }
        //auto_test键 选择是否删除
        public void button22_Click(object sender, EventArgs e)
        {
            //button2.PerformClick(); //触发GPS测试窗口
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //if (button2.ForeColor == Color.Blue && button3.ForeColor == Color.Blue)
            //{
            //    cnn.Open();
            //    string select = "select * from pt601 where SN=1112345618;";
            //    MySqlCommand select_1 = new MySqlCommand(select, cnn);
            //    if (select_1.ExecuteScalar() == null) //查询设备是否存在
            //    {
            //        MessageBox.Show("this is null");
            //    }
                
                //button23.Enabled = false;
                //string result = "insert into pt601 (SN, GPS, LED) values(12245618, 'pass', 'pass')";
                //MySqlCommand zhong = new MySqlCommand(result, cnn);
                //try
                //{
                //    zhong.ExecuteNonQuery();
                //}
                //catch
                //{
                //    MessageBox.Show("Updated successfully!");
                //    button23.Enabled = true;
                //}  
            //}
            //else
            //{
            //    MessageBox.Show("Please complete the functional test!");
            //}
        }

        public G_sensor f4 = new G_sensor();
        private void button4_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f4.ShowDialog();
                if (f4.isClick_g == true)
                {
                    button4.ForeColor = Color.Blue;
                }
                else
                {
                    button4.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public FLASH f5 = new FLASH();
        private void button5_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f5.ShowDialog();
                if (f5.isClick_f == true)
                {
                    button5.ForeColor = Color.Blue;
                }
                else
                {
                    button5.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public DS2483 f6 = new DS2483();
        private void button6_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f6.ShowDialog();
                if (f6.isClick_one == true)
                {
                    button6.ForeColor = Color.Blue;
                }
                else
                {
                    button6.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public H3LIS331DL f7 = new H3LIS331DL();
        private void button7_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f7.ShowDialog();
                if (f7.isClick_h3l == true)
                {
                    button7.ForeColor = Color.Blue;
                }
                else
                {
                    button7.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public BUZZER f8 = new BUZZER();
        private void button8_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f8.ShowDialog();
                if (f8.isClick_buz == true)
                {
                    button8.ForeColor = Color.Blue;
                }
                else
                {
                    button8.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public BAT f9 = new BAT();
        private void button9_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f9.ShowDialog();
                if (f9.isClick_bat == true)
                {
                    button9.ForeColor = Color.Blue;
                }
                else
                {
                    button9.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public OBD f10 = new OBD();
        private void button10_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f10.ShowDialog();
                if (f10.isClick_obd == true)
                {
                    button10.ForeColor = Color.Blue;
                }
                else
                {
                    button10.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public BLE f11 = new BLE();
        private void button11_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f11.ShowDialog();
                if (f11.isClick_ble == true)
                {
                    button11.ForeColor = Color.Blue;
                }
                else
                {
                    button11.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public LTE f13 = new LTE();
        private void button13_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f13.ShowDialog();
                if (f13.isClick_lte == true)
                {
                    button13.ForeColor = Color.Blue;
                }
                else
                {
                    button13.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }

        public version f12 = new version();
        private void button12_Click(object sender, EventArgs e)
        {
            if (isOpened)
            {
                f12.ShowDialog();
                if (f12.isClick_ver == true)
                {
                    button12.ForeColor = Color.Blue;
                }
                else
                {
                    button12.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("SerialPort Open Fail！");
            }
        }
    }
}