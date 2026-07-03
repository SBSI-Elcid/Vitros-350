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
                    _serialPort.DataReceived += DataReceivedHandler;
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing serial port: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string byteConverter(byte b)
        {
            // Translate invisible ASTM control bytes into readable labels
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



        private List<byte> _incomingBuffer = new List<byte>();
        private List<string> _currentSessionFrames = new List<string>();

        
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

                        continue;
                    }

                    if (b == 0x02) // (STX)

                    {
                        //_incomingBuffer.Clear();

                        continue;

                    }
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
                        continue;
                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading serial data: {ex.Message}");
            }
        }

        private void ProcessASTMFrame(List<string> sessionFrames)
        {
            foreach (string frameText in sessionFrames)
            {

                if (string.IsNullOrEmpty(frameText) || frameText.Length <= 5) continue;

                // --- CLEANING DIRECTLY AS A STRING ---
                int payloadLength = frameText.Length - 5;
                string cleanText = frameText;

                string[] records = cleanText.Split('\r');

               
                foreach (string record in records)
                {
                    if (string.IsNullOrWhiteSpace(record)) continue;

                    ParseASTMRecord(record);
                }
            }
        }

        private PatientInfoConfiguration PatientInfo = new PatientInfoConfiguration();
        private List<OrderInfoConfiguration> OrderInfo = new List<OrderInfoConfiguration>();

        private void ParseASTMRecord(string record)
        {
            Debug.WriteLine($"Processing record: {record}");
            if (string.IsNullOrWhiteSpace(record)) return;

            string[] fields = record.Trim().Split('|');

            fields = fields.Select(x => x.Trim()).ToArray();

            if (fields.Length == 0) return;

            string recordType = fields[0];

            switch (recordType)
            {
                case "H":

                    ProcessHeaderRecord(fields);
                    break;
                case "P":
                    ProcessPatientRecord(fields);
                    break;
                case "O":
                    ProcessOrderRecord(fields);
                    break;
                case "R":
                    ProcessResultRecord(fields);
                    break;
                case "C":
                    //ProcessCommentRecord(fields);
                    break;
                case "L":
                    //ProcessCommentRecord(fields);

                    //InsertIntoDatabase(PatientInfo, OrderInfo);
                    break;
                default:
                    Console.WriteLine($"Unknown record type: {recordType}");
                    break;
            }
        }

        private void ProcessHeaderRecord(string[] fields)
        {
            //if (fields.Length < 2) return;
            //string headerInfo = fields[1];
            //Debug.WriteLine($"Header Info: {headerInfo}");
        }

        private void ProcessPatientRecord(string[] fields)
        {
            
            PatientInfo.PatientID = fields.Length > 2 ? fields[2] : string.Empty;

            string rawPatientName = fields.Length > 5 ? fields[5] : string.Empty;

            string[] nameParts = rawPatientName.Split('^');

            string LastName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            string FirstName = nameParts.Length > 1 ? nameParts[1] : string.Empty;
            string MiddleInitial = nameParts.Length > 2 ? nameParts[2] : string.Empty;


            PatientInfo.PatientName = $"{LastName}, {FirstName} {MiddleInitial}".Trim();
            PatientInfo.PatientSex = fields.Length > 8 ? fields[8] : string.Empty;



            PatientInfo.PatientAddress = "";
            PatientInfo.DateOfBirth = fields.Length > 7 && DateTime.TryParseExact(fields[7], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dob) ? dob : DateTime.MinValue;

        }

        private void ProcessOrderRecord(string[] fields)
        {

            string rawSampleID = fields.Length > 2 ? fields[2] : string.Empty;
            string[] sampleIdParts = rawSampleID.Split('^');
            string SampleID = sampleIdParts.Length > 0 ? sampleIdParts[0] : string.Empty;
            List<string> channelCodes = new List<string>();


            OrderInfoConfiguration orderDetails = new OrderInfoConfiguration();


            PatientInfo.SampleID = SampleID;

            string fieldDetails = fields[4];
            string marker = "1.0+";
            int markerIndex = fieldDetails.IndexOf(marker);

            if (markerIndex != -1)
            {

                string channelsChunk = fieldDetails.Substring(markerIndex + marker.Length);


                char[] delimiters = new char[] { '+', '\\' };
                string[] tokens = channelsChunk.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                channelCodes = tokens.Where(t => t != "1").ToList();


            }

            foreach (string channel in channelCodes) {

                using (MySqlConnection conn = new MySqlConnection(config.DatabaseConnectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT specimen,test_code,his_code,his_field,si_unit,conventional_unit,section,test_name AS sub_section,test_group,instrument,order_no " +
                        "FROM specimen WHERE channel =@ChannelCode AND `status` = 'Enable'",
                        CommandType = CommandType.Text
                    };

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ChannelCode", channel);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) 
                    { 
                        while (reader.Read())
                        {
                            string specimen = reader["specimen"].ToString();
                            string testCode = reader["test_code"].ToString();
                            string hisCode = reader["his_code"].ToString();
                            string hisField = reader["his_field"].ToString();
                            string siUnit = reader["si_unit"].ToString();
                            string conventionalUnit = reader["conventional_unit"].ToString();
                            string section = reader["section"].ToString();
                            string subSection = reader["sub_section"].ToString();
                            string testGroup = reader["test_group"].ToString();
                            string instrument = reader["instrument"].ToString();
                            int orderNo = Convert.ToInt32(reader["order_no"]);


                            orderDetails = new OrderInfoConfiguration
                            {
                                SampleID = SampleID,
                                ChannelCode = channel,
                                SpecimenDate = fields.Length > 7 && DateTime.TryParseExact(fields[7], "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime specimenDateTime) ? specimenDateTime : DateTime.MinValue,
                                OrderSection = section,
                                OrderSubSection = subSection,
                                OrderName = specimen,
                                MachineName = instrument,
                                OrderType = "Out Patient",
                                PatientID = PatientInfo.PatientID,
                                PatientName = PatientInfo.PatientName,
                                PatientSex = PatientInfo.PatientSex,
                                PatientBday = PatientInfo.DateOfBirth,
                                OrderNo = orderNo
                            };

                            OrderInfo.Add(orderDetails);


                        }

                    };



                }
                

            }




        }

        private void ProcessResultRecord(string[] fields)
        {
            string rawChannelCodeValue = fields.Length > 2 ? fields[2] : string.Empty;
            string marker = "1.0+";
            int markerIndex = rawChannelCodeValue.IndexOf(marker);
            string code = String.Empty;
            string resultValue = String.Empty;

            if (markerIndex != -1)
            {

                string channelChunk = rawChannelCodeValue.Substring(markerIndex + marker.Length).Trim();

                string[] channelPart = channelChunk.Split('+');

                 code = channelPart.Length > 0 ? channelPart[0] : string.Empty;
            }

             resultValue = fields.Length > 3 ? fields[3] : string.Empty;

            OrderInfoConfiguration matchingConfig = OrderInfo
            .FirstOrDefault(config => config.ChannelCode == code);

            matchingConfig.OrderValue = resultValue ?? "";


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
