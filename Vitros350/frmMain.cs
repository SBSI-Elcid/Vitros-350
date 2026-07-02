using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vitros350.Classes;

namespace Vitros350
{
    public partial class frmMain : Form
    {
        private string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userconfiguration.json");

  

        private UserConfiguration userConfig = new UserConfiguration();
        private UserConfiguration.UserConfig config = new UserConfiguration.UserConfig();   

        public frmMain()
        {
            InitializeComponent();

            lblVersion.Text = $"Version {FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion}";

            string iconPath = Path.Combine(Application.StartupPath,"Icons", "Vitros350.ico");
            notifyIcon1.Icon = new Icon(iconPath); // Set your icon here
            notifyIcon1.Text = "Vitros350"; 
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
        }

        private void loadConfigSettings()
        {
            
            if (File.Exists(ConfigPath))
            {

                try
                {
                    
                    config = userConfig.loadConfigSettings();

                    //Server Settings
                    txtServerIp.Text = config.ServerIpAddress;
                    txtServerPort.Text = config.ServerPort;


                    //Database Settings
                    txtDatabaseName.Text = config.DatabaseName;
                    txtDatabasePass.Text = config.Password;
                    txtDatabasePort.Text = config.DatabasePort; 
                    txtDatabaseServer.Text = config.DatabaseServer;
                    txtDatabaseUsername.Text = config.Username;

                    //Machine Settings
                    txtMachineName.Text = config.MachineName;
                    cboComPort.Text = config.MachinePort;
                    txtMachineSerialNo.Text = config.SerialNumber;
                    txtMachineSection.Text = config.MachineSection;
                    txtMachineLocation.Text = config.MachineLocation;



                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                return;
            }
            return;
        }

        private void saveToSettings() {

            if (File.Exists(ConfigPath))
            {
                UserConfiguration.UserConfig config = new UserConfiguration.UserConfig
                {
                    //Server Settings
                    ServerIpAddress = txtServerIp.Text,
                    ServerPort = txtServerPort.Text,

                    //Database Settings
                    DatabaseName = txtDatabaseName.Text,
                    Username = txtDatabaseUsername.Text,
                    Password = txtDatabasePass.Text,
                    DatabasePort = txtDatabasePort.Text,
                    DatabaseServer = txtDatabaseServer.Text,

                    //Machine Settings
                    MachineName = txtMachineName.Text,
                    MachinePort = cboComPort.Text,
                    SerialNumber = txtMachineSerialNo.Text,
                    MachineSection = txtMachineSection.Text,
                    MachineLocation = txtMachineLocation.Text
                };
                try
                {
                    userConfig.saveToSettings(config);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void loadComboboxComPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            cboComPort.Items.Clear();
            cboComPort.Items.AddRange(ports);
        }

        private void loadAllSettings()
        {
            loadConfigSettings();
        }

        private SerialPort _serialPort = new SerialPort();

        private void InitiliazeSerialPort()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.PortName = config.MachinePort; 
                    _serialPort.BaudRate = 9600;
                    _serialPort.DataBits = 8;
                    _serialPort.Parity = Parity.None;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing serial port: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private List<byte> _incomingBuffer = new List<byte>();
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (sender as SerialPort);
            if (sp == null || !sp.IsOpen) return;

            try
            {

                int bytesToRead = sp.BytesToRead;
                byte[] incomingData = new byte[bytesToRead];

                sp.Read(incomingData, 0, bytesToRead);

                foreach (byte b in incomingData)
                {
                    // Check for start of frame (0x02 = STX)
                    if (b == 0x02)
                    {
                        _incomingBuffer.Clear();
                    }

                    _incomingBuffer.Add(b);

                    // Check for end of frame (0x03 = ETX or 0x17 = ETB)
                    if (b == 0x03 || b == 0x17)
                    {
                        byte[] frame = _incomingBuffer.ToArray();
                        _incomingBuffer.Clear();
                        ProcessASTMFrame(frame);
                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading serial data: {ex.Message}");
            }
        }

        private void ProcessASTMFrame(byte[] frameBytes)
        {
            int payloadLength = frameBytes.Length - 5;
            byte[] payloadBytes = frameBytes.Skip(1).Take(payloadLength).ToArray();

            string text = Encoding.ASCII.GetString(payloadBytes);

            string[] records = text.Split('\r');

            foreach (string record in records)
            {
                ParseASTMRecord(record);
            }
        }

        private void ParseASTMRecord(string record)
        {
            if (string.IsNullOrWhiteSpace(record)) return;

            string[] fields = record.Split('|');

            if (fields.Length == 0) return;

            string recordType = fields[0];

            Console.WriteLine($"Record: {record}");

            foreach (string field in fields)
            {
                Console.WriteLine($"Field: {field}");
            }




;
            switch (recordType)
            {
                case "H":
                    //ProcessHeaderRecord(fields);
                    break;
                case "P":
                    //ProcessPatientRecord(fields);
                    break;
                case "O":
                    //ProcessOrderRecord(fields);
                    break;
                case "R":
                    //ProcessResultRecord(fields);
                    break;
                case "C":
                    //ProcessCommentRecord(fields);
                    break;
                default:
                    Console.WriteLine($"Unknown record type: {recordType}");
                    break;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            saveToSettings();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000, "Vitros350", "Application minimized to tray.", ToolTipIcon.Info);
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadAllSettings();
            loadComboboxComPorts();
            InitiliazeSerialPort();


        }
    }
}
