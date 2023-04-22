using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Employee_Form.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmpID { get; set; }
       
        public string EmpName { get; set; }
        public DateTime EmpDOB { get; set; }
        public DateTime EmpDOJ { get; set; }
        public decimal EmpSalary { get; set; }
    }
}