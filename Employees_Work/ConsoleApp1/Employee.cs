using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Еmployee
    {
        private string empId;
        private string projectId;
        private DateTime dateFrom;
        private DateTime dateTo;

        public Еmployee(string empID, string projectID, DateTime dateFrom, DateTime dateTo)
        {
            EmpId = empID;
            ProjectId = projectID;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
        public string ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        public DateTime DateFrom
        {
            get { return dateFrom;}
            set { dateFrom = value; }
        }

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }
    }
}
