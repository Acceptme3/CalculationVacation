using CalculationVacation.Models;
using CalculationVacation.Services;
using CalculationVacation.Settings;
namespace CalculationVacation

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Employee> employees = new Dictionary<string, Employee>()
            {
                ["Иванов Иван Иванович"] = new Employee(),
                ["Петров Петр Петрович"] = new Employee(),
                ["Юлина Юлия Юлиановна"] = new Employee(),
                ["Сидоров Сидор Сидорович"] = new Employee(),
                ["Павлов Павел Павлович"] = new Employee(),
                ["Георгиев Георг Георгиевич"] = new Employee()
            };
            IServiceConfiguration configuration = new ServicesConfiguration();

            IEnumerable<DateTime> vacationSchedule = Employee.GetDateRange(new Vacation
            {
                BeginVacation = new DateTime(DateTime.Today.Year, 1, 1),
                EndOfVacation = new DateTime(DateTime.Today.Year, 12, 31)
            });

            VacationService service = new VacationService(configuration, employees, vacationSchedule);

            Dictionary<string, Employee> result = service.GetEmployeesVacation();

            Print();
            Console.ReadKey();

            void Print() 
            {
                Console.WriteLine($"Текущий график отпусков: ");
                foreach (var emp in result)
                { 
                    Console.WriteLine($"{emp.Key}");
                    foreach (var vac in emp.Value.Vacations)
                        Console.WriteLine($"Дата начала отпуска: {vac.BeginVacation} - Дата окончания отпуска: {vac.EndOfVacation}");
                }
            }
           
            
        }
    }
}
