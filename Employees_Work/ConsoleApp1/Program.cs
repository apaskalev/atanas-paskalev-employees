using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "EmployeesProjects.txt");

            Program progEmp = new Program();

            List<string> lines = progEmp.readFile(filePath);
            List<Еmployee> employees = progEmp.listOfEmp(lines);

            foreach (Еmployee emp in employees)
            {
                Console.WriteLine("Employee ID: {0}  Project ID: {1} Date from: {2} Date to: {3} \n", emp.EmpId, emp.ProjectId, emp.DateFrom.ToString("dd/MM/yyyy"), emp.DateTo.ToString("dd/MM/yyyy"));
            }
            Console.ForegroundColor = ConsoleColor.Green;
            progEmp.FindPair(employees);

            Console.ReadLine();
        }
        public List<string> readFile(string filePath)
        {
            List<string> linesFromFile = new List<string>();

            if (File.Exists(filePath))
            {
                linesFromFile = File.ReadAllLines(filePath).ToList();
                return linesFromFile;
            }
            else
            {
                Console.WriteLine("Input file is missing. Please create new one.");
                return linesFromFile;
            }
        }

        public List<Еmployee> listOfEmp(List<string> lines)
        {
            List<Еmployee> employees = new List<Еmployee>();

            foreach (string line in lines)
            {
                DateTime startDate;
                DateTime endDate;
                bool isParsable;
                string[] items = line.Split(',');

                isParsable = DateTime.TryParse(items[2], out startDate);
                if (!isParsable)
                    startDate = DateTime.Now;

                isParsable = DateTime.TryParse(items[3], out endDate);
                if (!isParsable)
                    endDate = DateTime.Now;

                Еmployee p = new Еmployee(items[0], items[1], startDate, endDate);
                employees.Add(p);
            }
            return employees;
        }
        public static double GetOverlappingDays(DateTime firstStart, DateTime firstEnd, DateTime secondStart, DateTime secondEnd)
        {
            DateTime maxStart = firstStart > secondStart ? firstStart : secondStart;
            DateTime minEnd = firstEnd < secondEnd ? firstEnd : secondEnd;
            TimeSpan interval = minEnd - maxStart;
            double returnValue = interval > TimeSpan.FromSeconds(0) ? interval.TotalDays : 0;
            return returnValue;
        }
        public void FindPair(List<Еmployee> employees)
        {
            double overlappingDays = 0;
            string firstEmployee = "";
            string secondEmployee = "";
            string projectID = "";

            foreach (Еmployee empFirst in employees)
            {
                foreach (Еmployee empSecond in employees)
                {
                    if (empFirst.EmpId != empSecond.EmpId && empFirst.ProjectId == empSecond.ProjectId)
                    {
                        if (empFirst.DateFrom < empSecond.DateTo && empSecond.DateFrom < empFirst.DateTo)
                        {
                            if (overlappingDays < GetOverlappingDays(empFirst.DateFrom, empFirst.DateTo, empSecond.DateFrom, empSecond.DateTo))
                            {
                                firstEmployee = empFirst.EmpId;
                                secondEmployee = empSecond.EmpId;
                                projectID = empSecond.ProjectId;
                                overlappingDays = GetOverlappingDays(empFirst.DateFrom, empFirst.DateTo, empSecond.DateFrom, empSecond.DateTo);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("The pair of employees who worked the longest on the same project are with IDs {0} and {1} on project ID{2}", firstEmployee, secondEmployee, projectID);
            Console.WriteLine("\nNumber of days working together: {0}", overlappingDays);
        }
    }
}
