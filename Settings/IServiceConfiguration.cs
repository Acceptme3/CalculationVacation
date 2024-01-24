using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationVacation.Settings
{
    internal interface IServiceConfiguration
    {
        public int MaxVacationDays { get; set; }
        public int[] VacationSize { get; set; }
        public List<DateTime> VacationDateSchedule { get; set; }
        public DateTime BeginYear { get; }
        public DateTime EndYear { get; }
        public List<string> Weeksdays { get; }
        public int DayOfYear
        {
            get
            {
                return (EndYear - BeginYear).Days;
            }
        }
    }
}
