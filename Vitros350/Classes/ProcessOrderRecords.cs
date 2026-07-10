using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Vitros350.Classes
{
    public class ProcessOrderRecords

    {
        private List<OrderInfoConfiguration> _orderInfo;
        private PatientInfoConfiguration _patientInfo;
        private UserConfiguration.UserConfig _config;
        public ProcessOrderRecords(List<OrderInfoConfiguration> OrderInfo, PatientInfoConfiguration PatientInfo, UserConfiguration.UserConfig config)
        {
            _orderInfo = OrderInfo ?? throw new ArgumentNullException(nameof(OrderInfo));
            _patientInfo = PatientInfo ?? throw new ArgumentNullException(nameof(PatientInfo));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ProcessHeaderRecord(string[] fields)
        {
            //if (fields.Length < 2) return;
            //string headerInfo = fields[1];
            //Debug.WriteLine($"Header Info: {headerInfo}");
        }

        public void ProcessPatientRecord(string[] fields)
        {
            _patientInfo.PatientID = fields.Length > 2 ? fields[2] : string.Empty;

            string rawPatientName = fields.Length > 5 ? fields[5] : string.Empty;

            string[] nameParts = rawPatientName.Split('^');

            string LastName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            string FirstName = nameParts.Length > 1 ? nameParts[1] : string.Empty;
            string MiddleInitial = nameParts.Length > 2 ? nameParts[2] : string.Empty;


            _patientInfo.PatientName = $"{LastName}, {FirstName} {MiddleInitial}".Trim();
            _patientInfo.PatientSex = fields.Length > 8 ? fields[8] : string.Empty;



            _patientInfo.PatientAddress = "";
            _patientInfo.DateOfBirth = fields.Length > 7 && DateTime.TryParseExact(fields[7], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dob) ? dob : DateTime.MinValue;

        }

        public void ProcessOrderRecord(string[] fields)
        {
            string rawSampleID = fields.Length > 2 ? fields[2] : string.Empty;
            string[] sampleIdParts = rawSampleID.Split('^');
            string SampleID = sampleIdParts.Length > 0 ? sampleIdParts[0] : string.Empty;
            List<string> channelCodes = new List<string>();


            _patientInfo.SampleID = SampleID;

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

            foreach (string channel in channelCodes)
            {



                using (MySqlConnection conn = new MySqlConnection(_config.DatabaseConnectionString))
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
                            OrderInfoConfiguration orderDetails = new OrderInfoConfiguration();

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
                                PatientID = _patientInfo.PatientID,
                                PatientName = _patientInfo.PatientName,
                                PatientSex = _patientInfo.PatientSex,
                                OrderTestGroup = testGroup,
                                PatientBday = _patientInfo.DateOfBirth,
                                OrderNo = orderNo,
                                OrderTestCode = testCode
                            };

                            _orderInfo.Add(orderDetails);
                        }

                    }
                }
            }
        }

        public void ProcessResultRecord(string[] fields)
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

            OrderInfoConfiguration matchingConfig = _orderInfo
            .FirstOrDefault(config => config.ChannelCode == code);

            if (matchingConfig != null)
            {
                matchingConfig.OrderValue = resultValue ?? "";
            }
        }

        public void InsertIntoDatabase()
        {

            DatabaseCrudOperations dbOperations = new DatabaseCrudOperations(_orderInfo, _patientInfo, _config);

            using (MySqlConnection conn = new MySqlConnection(_config.DatabaseConnectionString)) 
            { 
                conn.Open();

                using (MySqlTransaction databaseTransaction = conn.BeginTransaction())
                {
                    try 
                    {
                        dbOperations.CreateTmpworklistInfo(conn, databaseTransaction);
                        dbOperations.CreateSpecimenTrackingInfo(conn, databaseTransaction);
                        dbOperations.CreateResultInfo(conn, databaseTransaction);

                        databaseTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        databaseTransaction.Rollback();
                        ErrorLogger.LogError("An error occurred while inserting data into the database", ex);
                    }
                }
            }
        }
    }
}
