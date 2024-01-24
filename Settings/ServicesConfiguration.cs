using System.Dynamic;

namespace CalculationVacation.Settings
{
    internal class ServicesConfiguration : IServiceConfiguration
    {
        public int MaxVacationDays { get; set; } = 28;
        public int[] VacationSize { get; set; } =  [7, 14];
        public List<DateTime> VacationDateSchedule { get; set; } = new List<DateTime>();
        public DateTime BeginYear { get; } = new DateTime(DateTime.Today.Year, 1, 1);
        public DateTime EndYear { get; } = new DateTime(DateTime.Today.Year, 12, 31);
        public List<string> Weeksdays { get; } = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
        public int DayOfYear {
            get {
                return (EndYear - BeginYear).Days;
            } }
    }
}
