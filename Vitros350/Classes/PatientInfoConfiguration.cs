using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitros350.Classes
{
    public class PatientInfoConfiguration
    {
        //PatientInfo
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PatientSex { get; set; }
        public string PatientAddress { get; set; }

        //OrderInfo
        public string SampleID { get; set; }

    }
}
