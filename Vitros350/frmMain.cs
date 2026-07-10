using MySqlConnector;
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
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vitros350.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Vitros350
{
    public partial class frmMain : Form
    {
        private string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userconfiguration.json");


        private UserConfiguration.UserConfig config = new UserConfiguration.UserConfig();
        private UserConfiguration userConfig = new UserConfiguration();
        private SerialPort _serialPort = new SerialPort();
        private List<byte> _incomingBuffer = new List<byte>();
        private List<string> _currentSessionFrames = new List<string>();

        PatientInfoConfiguration PatientInfo = new PatientInfoConfiguration();
        List<OrderInfoConfiguration> OrderInfo = new List<OrderInfoConfiguration>();


        public frmMain()
        {
            InitializeComponent();

            lblVersion.Text = $"Version {FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion}";

            string iconPath = Path.Combine(Application.StartupPath, "Icons", "Vitros350.ico");
            notifyIcon1.Icon = new Icon(iconPath);
            notifyIcon1.Text = "Vitros350";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
        }


        private void saveToSettings()
        {

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
                    MachineLocation = txtMachineLocation.Text,

                    EnableTwoWayProcess = chkEnableTwoWayProcess.Checked
                };
                try
                {
                    userConfig.saveToSettings(config);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Error in saving userConfiguration:", ex);
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

                    chkEnableTwoWayProcess.Checked = chkEnableTwoWayProcess.Enabled && config.EnableTwoWayProcess;



                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Error in loading userConfiguration:", ex);
                }
            }
            else
            {
                return;
            }
            return;
        }

        public void addToTraceComm(string Message) 
        {
            string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TraceComm.txt");

            try
            {
                // AppendText handles both creating a new file and appending to an existing one
                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    sw.WriteLine(Message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Logging failed: {ex.Message}");
            }

        }

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
                    _serialPort.DataReceived += DataReceivedHandler;
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Error in initializing Serial Port:", ex);
            }
        }

        private string byteConverter(byte b)
        {
            // Translate ASTM bytes into labels
            switch (b)
            {
                case 0x02: return "<STX>";
                case 0x03: return "<ETX>";
                case 0x04: return "<EOT>";
                case 0x05: return "<ENQ>";
                case 0x06: return "<ACK>";
                case 0x15: return "<NAK>";
                case 0x17: return "<ETB>";
                case 0x0D: return "<CR>";
                case 0x0A: return "<LF>\n";
                default:
                    return ((char)b).ToString();
            }
        }

        private string logsData(string frameText)
        {
            if (string.IsNullOrEmpty(frameText)) return string.Empty;

            return frameText
                .Replace("\x02", "<STX>")
                .Replace("\x03", "<ETX>")
                .Replace("\x04", "<EOT>")
                .Replace("\x05", "<ENQ>")
                .Replace("\x06", "<ACK>")
                .Replace("\x15", "<NAK>")
                .Replace("\x17", "<ETB>")
                .Replace("\r", "<CR>")
                .Replace("\n", "<LF>");
        }

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

                    if (b == 0x05) // ENQ   
                    {

                        _currentSessionFrames.Clear();
                        _incomingBuffer.Clear();
                        sp.Write(new byte[] { 0x06 }, 0, 1); // Send ACK in response to ENQ

                        this.Invoke((MethodInvoker)delegate
                        {
                            rTextLogs.Clear();
                            rTextLogs.AppendText("******************************Start-of-Communication*******************************\r\n");
                            rTextLogs.AppendText($"{DateTime.Now} [Machine]: {byteConverter(b)}\r\n");
                            rTextLogs.AppendText($"{DateTime.Now} [Host]: {byteConverter(0x06)} \r\n");
                        });

                        addToTraceComm($"{DateTime.Now} [Machine]: {byteConverter(b)}\r\n");

                        continue;
                    }

                    //if (b == 0x02) // (STX)

                    //{
                    //    //_incomingBuffer.Clear();

                    //    continue;

                    //}
                    _incomingBuffer.Add(b);

                    if (b == 0x0A) // LF
                    {
                        if (_incomingBuffer.Contains(0x03) || _incomingBuffer.Contains(0x17)) // Check for ETX or ETB
                        {
                            byte[] frameBytes = _incomingBuffer.ToArray();
                            _incomingBuffer.Clear();

                            string frameText = Encoding.ASCII.GetString(frameBytes);
                            _currentSessionFrames.Add(frameText);

                            sp.Write(new byte[] { 0x06 }, 0, 1); // Send ACK in response to ETX or ETB



                            this.Invoke((MethodInvoker)delegate
                            {
                                rTextLogs.AppendText($"{DateTime.Now} [Machine]: {logsData(frameText)} \r\n");
                                rTextLogs.AppendText($"{DateTime.Now} [Host]: {byteConverter(0x06)} \r\n");
                            });

                            addToTraceComm($"{DateTime.Now} [Machine]: {logsData(frameText)}\r\n");
                        }
                    }

                    if (b == 0x04) // EOT
                    {


                        ProcessASTMFrame(_currentSessionFrames);

                        _currentSessionFrames.Clear();

                        this.Invoke((MethodInvoker)delegate
                        {
                            rTextLogs.AppendText($"******************************End-of-Communication*******************************\r\n");

                        });

                        addToTraceComm($"{DateTime.Now} [Machine]: {byteConverter(0x04)}\r\n");
                        continue;
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorLogger.LogError("Error in reading Serial Data:", ex);
            }
        }

        public string cleanedText(string RawText) {

            string cleanText = string.Empty;

            try
            {
                
                 cleanText = RawText.Trim('\u0002', '\u0003', '\u0017', '\r', '\n');

                if (cleanText.Length > 0 && char.IsDigit(cleanText[0]))
                {
                    cleanText = cleanText.Substring(1);
                }

            }
            catch (Exception ex) {
                ErrorLogger.LogError("Error in Cleaning Text:", ex);
            }

            return cleanText;


        }

        private void ProcessASTMFrame(List<string> sessionFrames)
        {
            try 
            {
                foreach (string frameText in sessionFrames)
                {

                    if (string.IsNullOrEmpty(frameText) || frameText.Length <= 5) continue;

                    // --- CLEANING DIRECTLY AS A STRING ---
                    int payloadLength = frameText.Length - 5;
                    string cleanText = cleanedText(frameText);

                    string[] records = cleanText.Split('\r');


                    foreach (string record in records)
                    {
                        if (string.IsNullOrWhiteSpace(record)) continue;

                        ProcessASTMRecord(record);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Error in ProcessASTM Frame/Record:", ex);
            }

        }


        private void ProcessASTMRecord(string record)
        {

            ProcessOrderRecords processOrderRecords = new ProcessOrderRecords(OrderInfo, PatientInfo, config);

            if (string.IsNullOrWhiteSpace(record)) return;

            string[] fields = record.Trim().Split('|');

            fields = fields.Select(x => x.Trim()).ToArray();

            if (fields.Length == 0) return;

            string recordType = fields[0];

            switch (recordType)
            {
                case "H":

                    processOrderRecords.ProcessHeaderRecord(fields);
                    break;
                case "P":

                    processOrderRecords.ProcessPatientRecord(fields);
                    break;
                case "O":
                    processOrderRecords.ProcessOrderRecord(fields);
                    break;
                case "R":
                    processOrderRecords.ProcessResultRecord(fields);
                    break;
                case "C":
                    break;
                case "L":
                    processOrderRecords.InsertIntoDatabase();

                    PatientInfo = new PatientInfoConfiguration();
                    OrderInfo = new List<OrderInfoConfiguration>();

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

        private void btnConnect_Click(object sender, EventArgs e)
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
                    _serialPort.DataReceived += DataReceivedHandler;
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing serial port: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rTextLogs.Clear();
        }
    }
}
