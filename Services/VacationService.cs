using CalculationVacation.Models;
using CalculationVacation.Settings;
using System;

namespace CalculationVacation.Services
{
    internal class VacationService : IVacationService
    {
        private readonly IServiceConfiguration _configuration;
        private readonly Dictionary<string,Employee> _employees;
        private readonly IEnumerable<DateTime> _vacationSchedule;

        Random rnd = new Random();
        List<DateTime> vacationScheduleList;
        Dictionary<string,Employee> employeesList;
        public VacationService(IServiceConfiguration configuration, Dictionary<string,Employee> employees, IEnumerable<DateTime> vacationSchedule)
        {
            _configuration = configuration;
            _employees = employees;
            _vacationSchedule = vacationSchedule;

            vacationScheduleList = _vacationSchedule.ToList();
            employeesList = _employees;
        }

        public Dictionary<string, Employee> GetEmployeesVacation()
        {
            Dictionary<string,Employee> resultDictionary = new Dictionary<string, Employee>();

            if (employeesList == null) { Console.WriteLine("Список сотрудников пуст"); return resultDictionary; }
            if (vacationScheduleList == null) { Console.WriteLine("Список свободных дат отпусков пуст"); return resultDictionary; }

            Employee resultExample = new Employee();

            while (employeesList.Count() > 0)
            {
                if (vacationScheduleList.Count() < _configuration.VacationSize.Min())
                {
                    Console.WriteLine("Список доступных дат исчерпан");
                    break;
                }

                resultExample = _employees.OrderBy(e => rnd.Next()).Select(e=>e.Value).FirstOrDefault()!;
                if (resultExample.GetCountVacationDays() == 28)
                {
                    List<Employee> employeesForDel = new List<Employee>() { resultExample };
                    RemoveElements<Employee>(employeesList, employeesForDel);
                    continue;
                }

                Vacation vacation = GetVacation();
                if (!IsValidVacation(vacation))
                {
                    continue;
                }
                resultExample.Vacations.Add(GetVacation());
                RemoveElements<DateTime>(vacationScheduleList, Employee.GetDateRange(vacation));
                AddOrUpdateEmployee(resultDictionary, resultExample);
            }
            return resultDictionary;

            bool IsValidVacation(Vacation _vacation)
            {
                foreach (var item in resultExample.Vacations)
                {
                    if (item.Equals(_vacation))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        void AddOrUpdateEmployee(Dictionary<string, Employee> employees, Employee employee)
        {
            if (employees.ContainsKey(employee.FullName))
            {
                employees[employee.FullName] = employee;
            }
            else
            {
                employees.Add(employee.FullName, employee);
            }
        }

        Vacation GetVacation()
        {
            Vacation vacationExample = new Vacation();
            bool completed = false;
            while (!completed)
            {
                vacationExample.BeginVacation = _vacationSchedule.First();
                if (!_configuration.Weeksdays.Contains(vacationExample.BeginVacation.DayOfWeek.ToString()))
                {
                    continue;
                }
                int daysCount = _configuration.VacationSize[rnd.Next(_configuration.VacationSize.Length)];
                vacationExample.EndOfVacation = vacationExample.BeginVacation.AddDays(daysCount);
            }
            return vacationExample;
        }
        

        void RemoveElements<T>(ICollection<T> collection, IEnumerable<T> elementsToRemove)
        {
            foreach (var element in elementsToRemove)
            {
                collection = collection.Where(item => !item.Equals(element)).ToList();
            }
        }

        void RemoveElements<T>(Dictionary<string, Employee> collection, IEnumerable<Employee> elementsToRemove)
        {
            foreach (var element in elementsToRemove.ToList())
            {
                if (collection.ContainsKey(element.FullName))
                {
                    collection.Remove(element.FullName);
                }
            }
        }
    }
}

