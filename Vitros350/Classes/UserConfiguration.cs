using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vitros350.Classes
{

    public class UserConfiguration
    {

        public class UserConfig

        {

            //Server settings
            public string ServerIpAddress { get; set; }
            public string ServerPort { get; set; }


            //Database settings
            public string DatabaseServer { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string DatabasePort { get; set; }
            public string DatabaseName { get; set; }

            public string DatabaseConnectionString
            {
                get
                {
                    return $"Server={DatabaseServer};Port={DatabasePort};Database={DatabaseName};Uid={Username};Pwd={Password};";
                }
            }

            //Machine settings
            public string MachineName { get; set; }
            public string MachinePort { get; set; }
            public string SerialNumber { get; set; }
            public string MachineSection { get; set; }
            public string MachineLocation { get; set; }

            public bool EnableTwoWayProcess { get; set; }
        }


        public string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userconfiguration.json");


        public UserConfig loadConfigSettings()
        {
            UserConfig config = new UserConfig();

            if (File.Exists(ConfigPath))
            {


                try
                {
                    string json = File.ReadAllText(ConfigPath);
                    config = JsonConvert.DeserializeObject<UserConfig>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return new UserConfig();
            }
            return config;
        }

        public void saveToSettings(UserConfig configDetails)
        {

            if (File.Exists(ConfigPath))
            {
                UserConfig config = new UserConfig
                {
                    //Server Settings
                    ServerIpAddress = configDetails.ServerIpAddress,
                    ServerPort = configDetails.ServerPort,

                    //Database Settings
                    DatabaseName = configDetails.DatabaseName,
                    Username = configDetails.Username,
                    Password = configDetails.Password,
                    DatabasePort = configDetails.DatabasePort,
                    DatabaseServer = configDetails.DatabaseServer,

                    //Machine Settings
                    MachineName = configDetails.MachineName,
                    MachinePort = configDetails.MachinePort,
                    SerialNumber = configDetails.SerialNumber,
                    MachineSection = configDetails.MachineSection,
                    MachineLocation = configDetails.MachineLocation,

                    EnableTwoWayProcess = configDetails.EnableTwoWayProcess

                    
                };

                try
                {
                    string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                    File.WriteAllText(ConfigPath, json);
                    MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
