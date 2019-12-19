using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Employment
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Manager { get; set; }
        public DateTime HireDate { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Non Satisfing Value")]
        public int Salary { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Non Satisfing Value")]
        public int? Commission { get; set; }

        public int DepartamentId { get; set; }

        [ForeignKey("DepartamentId")]
        public Department Department { get; set; }
    }
}
