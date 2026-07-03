using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitros350.Classes
{
    public class OrderInfoConfiguration
    {
        public string SampleID { get; set; }
        public string ChannelCode { get; set; }
        public DateTime SpecimenDate { get; set; }
        public string OrderSection { get; set; }
        public string OrderSubSection { get; set; }
        public string OrderName { get; set; }
        public string MachineName { get; set; }
        public string OrderType { get; set; }
        public int OrderNo { get; set; }
        public string OrderValue { get; set; }




        //OtherRequiredInfo
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public DateTime PatientBday { get; set; }


        public int PatientAge
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - PatientBday.Year;
                if (PatientBday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public string PatientType
        {
            get
            {
                var today = DateTime.Today;

                // Safety check for empty or invalid future dates
                if (PatientBday == DateTime.MinValue || PatientBday > today)
                {
                    return "Unknown";
                }

                // 1. Calculate age differences
                int totalDays = (today - PatientBday).Days;

                int years = today.Year - PatientBday.Year;
                int months = today.Month - PatientBday.Month;

                // Adjust months/years if we haven't crossed their exact birth day yet this month
                if (today.Day < PatientBday.Day)
                {
                    months--;
                }
                if (months < 0)
                {
                    months += 12;
                    years--;
                }

                int totalMonths = (years * 12) + months;

                // 2. Classify based on age thresholds
                // Case A: Below a month old (0 months) -> Display in Days
                if (totalMonths < 1)
                {
                    return $"Day(s)";
                }
                // Case B: Below a year old (1 to 11 months) -> Display in Months
                else if (totalMonths < 12)
                {
                    return $"Month(s)";
                }
                // Case C: 1 Year or older -> Display in Years
                else
                {
                    return $"Year(s)";
                }
            }
        }

    }
}
