using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Employment.Task
{
    public class Task_2
    {
        public static void Quest_1(EmployeeContext db)
        {
            var salgrades = db.SalGrades.ToList();

            foreach(var sg in salgrades)
                Console.WriteLine($"GradeId:{sg.SalGradeId} Losal:{sg.Losal} Hisal:{sg.Hisal}");
        }

        public static void Quest_2(EmployeeContext db) {
            var employees = db.Employees.Select(e=> new
            {
                e.Name,
                e.DepartamentId,
                e.Salary
            })
                .Where(e => e.Salary > 1000 && e.Salary < 2000).ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Department:{e.DepartamentId} Salary:{e.Salary}");
        }

        public static void Quest_3(EmployeeContext db) {
            var departments = db.Departments.Select(e=> new
            {
                e.Name,
                e.DepartmentId
            }).OrderBy(d=>d.DepartmentId).ToList();

            foreach (var d in departments)
                Console.WriteLine($"DepartmentId:{d.DepartmentId} Name:{d.Name}");
        }

        public static void Quest_4(EmployeeContext db) {
            var jobs = db.Employees.Select(e=>e.Job).Distinct().ToList();

            foreach(var j in jobs) { 
                Console.WriteLine($"Job:{j}");
            }
        }

        public static void Quest_5(EmployeeContext db) {
            var employees = db.Employees
                .Where(e => e.DepartamentId == 10 || e.DepartamentId == 20)
                .OrderBy(e => e.Job)
                .ToList();

            foreach(var e in employees)
                Console.WriteLine($"EmployeeId:{e.EmployeeId} Name:{e.Name} Job:{e.Job} Manager:{e.Manager} Hiredate:{e.HireDate} Salary:{e.Salary} Commission:{e.Commission} DepartmentId:{e.DepartamentId}");
        }

        public static void Quest_6(EmployeeContext db) {
            var employees = (from emp in db.Employees
                             where emp.DepartamentId == 20 && emp.Job == "CLERK"
                             select emp.Name
                             ).ToList();

            foreach(var name in employees)
                Console.WriteLine($"Name:{name}");
        }

        public static void Quest_7(EmployeeContext db) {
            var employees = db.Employees
                .Where(e=> EF.Functions.Like(e.Name,"%TH%") || EF.Functions.Like(e.Name, "%LL%")).ToList();
                
            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name}");
        }

        public static void Quest_8(EmployeeContext db) {
            var employees = db.Employees.Select(e => new
            {
                e.Name,
                e.Salary
            })
            .Where(e => (e.Salary < 1000 || e.Salary > 2000) && e.Name.Length == 5)
            .ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name}");
        }

        public static void Quest_9(EmployeeContext db) {
            int[] salary = { 800, 1600, 3000 };
            var employees = from emp in db.Employees
                            where !salary.Contains(emp.Salary) && !EF.Functions.Like(emp.Name, "%L%")
                            select new { emp.Name, emp.Salary };

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_10(EmployeeContext db) {
            var employees = db.Employees.Select(e => new
            {
                e.Name,
                e.Salary,
                e.Job,
                e.Manager
            }).Where(e => e.Manager!=0)
            .ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Salary:{e.Salary} Job:{e.Job}");
        }

        public static void Quest_11(EmployeeContext db) {
            var employees = db.Employees.Select(e=>new {
                e.Name,
                e.Job,
                Annasal=12*e.Salary +e.Commission
            })
                .Where(e => e.Job.Equals("SALESMAN")).ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Income:{e.Annasal}");
        }

        public static void Quest_12(EmployeeContext db) {
            var employees = db.Employees.Where(e => e.Job == "Manager" && e.DepartamentId == 10);

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Job:{e.Job} Department:{e.DepartamentId}");
        }

        public static void Quest_13(EmployeeContext db) {
            var employees = db.Employees
                .Where(e => DateTime.Compare(e.HireDate, new DateTime(1983, 1, 1)) >= 0)
                .ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name {e.Name} Hiredate:{e.HireDate}");
        }

        public static void Quest_14(EmployeeContext db) {
            var employees = db.Employees
                .Where(e => e.Job.Equals("SALESMAN") && e.Salary > e.Commission)
                .OrderByDescending(e=>e.Salary)
                .ThenBy(e=>e.Name).ToList();

            foreach(var e in employees)
                Console.WriteLine($"Name:{e.Name} Income:{12*e.Salary+e.Commission} Comission:{e.Commission}");
        }

        public static void Quest_15(EmployeeContext db) {
            foreach(var e in db.Employees.ToList())
                Console.WriteLine($"{e.Name} has held the position of {e.Job} in {e.DepartamentId} since {e.HireDate.Date.ToShortDateString()}");
        }
    }
}
