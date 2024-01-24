using System.Diagnostics.CodeAnalysis;

namespace CalculationVacation.Models
{
    internal class Employee
    {
        public string FullName { get; set; } = string.Empty;
        public List<Vacation> Vacations { get; set; } = new List<Vacation>();

        public int GetCountVacationDays() 
        {
            int result = 0;
            foreach (var vacation in Vacations)
            {
                result += vacation.VacationDaysCount();
            }
            return result;
        }
        public static IEnumerable<DateTime> GetDateRange(Vacation vacation)
        {
            List<DateTime> dateRange = new List<DateTime>();

            for (DateTime date = vacation.BeginVacation; date <= vacation.EndOfVacation; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }
            return dateRange;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Employee other = (Employee)obj;
            return string.Equals(FullName, other.FullName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(FullName);
        }
    }




    internal struct Vacation
    {
        public DateTime BeginVacation { get; set; }
        public DateTime EndOfVacation { get; set; }

        public int VacationDaysCount()
        {
            TimeSpan difference = EndOfVacation - BeginVacation;
            return difference.Days;
        }

        public bool Equals(Vacation vacation)
        {
            bool isIntersecting = BeginVacation <= vacation.EndOfVacation && EndOfVacation >= vacation.BeginVacation;

            bool isNested = BeginVacation >= vacation.BeginVacation && EndOfVacation <= vacation.EndOfVacation;

            return isIntersecting || isNested;
        }
    }
}
