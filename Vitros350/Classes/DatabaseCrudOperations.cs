using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitros350.Classes
{
    public class DatabaseCrudOperations
    {
        private List<OrderInfoConfiguration> _orderInfo;
        private PatientInfoConfiguration _patientInfo;
        private UserConfiguration.UserConfig _config;

        public DatabaseCrudOperations(List<OrderInfoConfiguration> OrderInfo, PatientInfoConfiguration PatientInfo, UserConfiguration.UserConfig config)
        {

            _orderInfo = OrderInfo ?? throw new ArgumentNullException(nameof(OrderInfo));
            _patientInfo = PatientInfo ?? throw new ArgumentNullException(nameof(PatientInfo));
            _config = config ?? throw new ArgumentNullException(nameof(config));

        }

        public void CreateTmpworklistInfo(MySqlConnection conn, MySqlTransaction transaction)
        {
            bool isRecordExists = false;

            List<OrderInfoConfiguration> tmpWorklist = _orderInfo.GroupBy(o => new { o.OrderSection, o.OrderSubSection })
                .Select(g => g.First())
                .ToList();

            foreach (var order in tmpWorklist)
            {


                using (MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT 1 FROM tmpworklist WHERE main_id = @SampleID AND testtype = @Section AND sub_section = @SubSection",
                    CommandType = CommandType.Text

                })

                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                    cmd.Parameters.AddWithValue("@Section", order.OrderSection);
                    cmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            isRecordExists = true;
                        }
                    }
                }

                if (!isRecordExists)
                {

                    using (MySqlCommand insertCmd = new MySqlCommand
                    {
                        Connection = conn,
                        Transaction = transaction,
                        CommandText = "INSERT INTO tmpworklist (`status`, sample_id, patient_id,patient_name,sex,bdate,age,`date`,`time`,test,patient_type,testtype,instrument,main_id,TYPE,location,sub_section)" +
                        " VALUES (@Status, @SampleID, @PatientID,@PatientName,@Sex,@BDate,@Age,@Date,@Time,@Tests,@PatientType,@Section,@Instrument,@SampleID,@Type,@Location,@SubSection) ",
                        CommandType = CommandType.Text
                    })
                    {
                        insertCmd.Parameters.Clear();
                        insertCmd.Parameters.AddWithValue("@Status", "Result Received");
                        insertCmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                        insertCmd.Parameters.AddWithValue("@PatientID", order.PatientID);
                        insertCmd.Parameters.AddWithValue("@PatientName", order.PatientName);
                        insertCmd.Parameters.AddWithValue("@Sex", order.PatientSex);
                        insertCmd.Parameters.AddWithValue("@BDate", order.PatientBday.ToString("MM-dd-yyyy"));
                        insertCmd.Parameters.AddWithValue("@Age", order.PatientAge);
                        insertCmd.Parameters.AddWithValue("@Date", order.SpecimenDate.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@Time", order.SpecimenDate.ToString("HH:mm:ss"));
                        insertCmd.Parameters.AddWithValue("@Tests", order.OrderName);
                        insertCmd.Parameters.AddWithValue("@PatientType", order.PatientType);
                        insertCmd.Parameters.AddWithValue("@Type", "Out Patient");
                        insertCmd.Parameters.AddWithValue("@Section", order.OrderSection);
                        insertCmd.Parameters.AddWithValue("@Instrument", order.MachineName);
                        insertCmd.Parameters.AddWithValue("@Location", "Laboratory");
                        insertCmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);

                        insertCmd.ExecuteNonQuery();

                    }
                }
                else
                {
                    using (MySqlCommand updateCmd = new MySqlCommand
                    {
                        Connection = conn,
                        Transaction = transaction,
                        CommandText = "UPDATE tmpworklist SET `status` = @Status, patient_id = @PatientID, patient_name = @PatientName, sex = @Sex, bdate = @BDate, age = @Age, `date` = @Date, `time` = @Time, " +
                        "test = @Tests, patient_type = @PatientType, instrument = @Instrument, sample_id = @SampleID, TYPE =@Type, location = @Location WHERE main_id = @SampleID AND testtype = @Section AND sub_section = @SubSection",
                        CommandType = CommandType.Text
                    })
                    {
                        updateCmd.Parameters.Clear();
                        updateCmd.Parameters.AddWithValue("@Status", "Result Received");
                        updateCmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                        updateCmd.Parameters.AddWithValue("@PatientID", order.PatientID);
                        updateCmd.Parameters.AddWithValue("@PatientName", order.PatientName);
                        updateCmd.Parameters.AddWithValue("@Sex", order.PatientSex);
                        updateCmd.Parameters.AddWithValue("@BDate", order.PatientBday.ToString("MM-dd-yyyy"));
                        updateCmd.Parameters.AddWithValue("@Age", order.PatientAge);
                        updateCmd.Parameters.AddWithValue("@Date", order.SpecimenDate.ToString("yyyy-MM-dd"));
                        updateCmd.Parameters.AddWithValue("@Time", order.SpecimenDate.ToString("HH:mm:ss"));
                        updateCmd.Parameters.AddWithValue("@Tests", order.OrderName);
                        updateCmd.Parameters.AddWithValue("@PatientType", order.PatientType);
                        updateCmd.Parameters.AddWithValue("@Type", "Out Patient");
                        updateCmd.Parameters.AddWithValue("@Section", order.OrderSection);
                        updateCmd.Parameters.AddWithValue("@Instrument", order.MachineName);
                        updateCmd.Parameters.AddWithValue("@Location", "Laboratory");
                        updateCmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);

                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
        }

        public void CreateSpecimenTrackingInfo(MySqlConnection conn, MySqlTransaction transaction)
        {
            bool isRecordExists = false;

            List<OrderInfoConfiguration> tmpWorklist = _orderInfo.GroupBy(o => new { o.OrderSection, o.OrderSubSection })
                .Select(g => g.First())
                .ToList();

            foreach (var order in tmpWorklist)
            {
                using (MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT 1 FROM specimen_tracking WHERE sample_id = @SampleID AND section = @Section AND sub_section = @SubSection",
                    CommandType = CommandType.Text

                })

                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                    cmd.Parameters.AddWithValue("@Section", order.OrderSection);
                    cmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            isRecordExists = true;
                        }
                    }
                }

                if (!isRecordExists)
                {

                    using (MySqlCommand insertCmd = new MySqlCommand
                    {
                        Connection = conn,
                        Transaction = transaction,
                        CommandText = "INSERT INTO specimen_tracking (`sample_id`,received,extracted,processing,section,sub_section)" +
                        " VALUES (@SampleID,@Date,@Date,@Date,@Section,@SubSection) ",
                        CommandType = CommandType.Text
                    })
                    {
                        insertCmd.Parameters.Clear();
                        insertCmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                        insertCmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        insertCmd.Parameters.AddWithValue("@Section", order.OrderSection);
                        insertCmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);
                        insertCmd.ExecuteNonQuery();

                    }
                }
            }


        }

        public void CreateResultInfo(MySqlConnection conn, MySqlTransaction transaction)
        {

            bool isRecordExists = false;
            string SQL = String.Empty;

            foreach (var order in _orderInfo)
            {
                using (MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT 1 FROM tmpresult WHERE sample_id = @SampleID AND section = @Section AND sub_section = @SubSection AND test_code = @TestCode",
                    CommandType = CommandType.Text
                })
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                    cmd.Parameters.AddWithValue("@Section", order.OrderSection);
                    cmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);
                    cmd.Parameters.AddWithValue("@TestCode", order.OrderTestCode);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            isRecordExists = true;
                        }
                    }

                }

                if (isRecordExists)
                {
                    SQL = "INSERT INTO rerun_result(universal_id,measurement,test_code,order_no,sample_id,test_group,`date`,patient_id,instrument,`status`,section,sub_section) VALUES " +
                        "(@SpecimenName,@ResultValue,@TestCode,@OrderNo,@SampleID,@TestGroup,@Date,@PatientID,@Instrument,(SELECT COALESCE(MAX(`status`), 0) + 1 FROM (SELECT `status` FROM rerun_result) AS temp),@Section,@SubSection)";
                }
                else
                {
                    SQL = "INSERT INTO tmpresult(universal_id,measurement,test_code,order_no,sample_id,test_group,`date`,patient_id,instrument,`status`,his_code,section,sub_section,print_status) VALUES " +
                        "(@SpecimenName,@ResultValue,@TestCode,@OrderNo,@SampleID,@TestGroup,@Date,@PatientID,@Instrument,@StatusNo,@HisCode,@Section,@SubSection,1);" +
                        "INSERT INTO rerun_result(universal_id,measurement,test_code,order_no,sample_id,test_group,`date`,patient_id,instrument,`status`,section,sub_section) VALUES " +
                        "(@SpecimenName,@ResultValue,@TestCode,@OrderNo,@SampleID,@TestGroup,@Date,@PatientID,@Instrument,(SELECT COALESCE(MAX(`status`), 0) + 1 FROM (SELECT `status` FROM rerun_result) AS temp),@Section,@SubSection)";
                }

                using (MySqlCommand upsertCmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = SQL,
                    Transaction = transaction,
                    CommandType = CommandType.Text
                })
                {
                    upsertCmd.Parameters.Clear();
                    upsertCmd.Parameters.AddWithValue("@SpecimenName", order.OrderName);
                    upsertCmd.Parameters.AddWithValue("@ResultValue", order.OrderValue);
                    upsertCmd.Parameters.AddWithValue("@TestCode", order.OrderTestCode);
                    upsertCmd.Parameters.AddWithValue("@OrderNo", order.OrderNo);
                    upsertCmd.Parameters.AddWithValue("@SampleID", order.SampleID);
                    upsertCmd.Parameters.AddWithValue("@TestGroup", order.OrderTestGroup);
                    upsertCmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    upsertCmd.Parameters.AddWithValue("@PatientID", order.PatientID);
                    upsertCmd.Parameters.AddWithValue("@Instrument", order.MachineName);
                    upsertCmd.Parameters.AddWithValue("@StatusNo", 0);
                    upsertCmd.Parameters.AddWithValue("@HisCode", order.OrderHisCode);
                    upsertCmd.Parameters.AddWithValue("@Section", order.OrderSection);
                    upsertCmd.Parameters.AddWithValue("@SubSection", order.OrderSubSection);
                    upsertCmd.ExecuteNonQuery();
                }
            }
        }

    }
}
