﻿namespace EmployeeReports.Server.Models
{
    public class EmployeeProjectRecord
    {
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
