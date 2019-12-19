using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employment.Task
{
    public class Task_1
    {
        public static void Quest_1(EmployeeContext db)
        {
            var cities = db.Employees.Include(e => e.Department)
                .GroupBy(e => e.Department.City)
                .Select(c=>new {
                    City=c.Key,
                    Date=c.Min(d=>d.HireDate)
                });
            
            foreach(var c in cities)
            {
                Console.WriteLine($"City:{c.City} Date:{c.Date}");
            }
        }

        public static void Quest_2(EmployeeContext db) {

            var salaries = db.Employees.Include(e => e.Department)
                .Where(e => e.Department.City == "DALLAS")
                .Select(e=>e.Salary).ToList();

            var empolyees = db.Employees.Include(e=>e.Department)
            .Where(e => e.Department.City != "DALLAS");

           foreach (var e in empolyees)
           {
                if (CompareSalaries(e.Salary, salaries))
                {
                    Console.WriteLine($"Name:{e.Name} Salary:{e.Salary}");
                }
           }
        }

        public static void Quest_3(EmployeeContext db) {
            var departments = db.Employees.Include(e => e.Department)
                .Where(e=>e.Name.Length==4)
                .GroupBy(e=>e.DepartamentId)
                .Select(d=>new {
                    Department = d.Key,
                    Staffs = d.Count()
                });
            foreach(var d in departments)
                Console.WriteLine($"Department:{d.Department} Staffs:{d.Staffs}");
        }

        public static void Quest_4(EmployeeContext db) {
            var departments = db.Employees.Include(e => e.Department).Where(a => 
                      a.Salary ==((int)db.Employees.Join(db.SalGrades,
                      e => 1,
                      s => 1,
                      (e, s) => new
                      {
                          e.Salary,
                          s.Hisal,
                          s.Losal,
                          s.SalGradeId
                      }).Where(e => e.Salary >= e.Losal && e.Salary <= e.Hisal && e.SalGradeId == 1)
                        .Average(e => e.Salary)));
                
            foreach(var d in departments)
                Console.WriteLine($"Department:{d.Department.Name} Employee:{d.Name} Salary:{d.Salary}");
                 
        }

        public static void Quest_5(EmployeeContext db) {
            var supervisor = db.Employees.Join(db.Employees,
                s => s.EmployeeId,
                e => e.Manager,
                (s, e) => new {
                    s.Name,
                    Emloyees = e.Name.Count()
                }
                ).GroupBy(s=>s.Name);

            foreach (var s in supervisor) { 
                Console.WriteLine(s.Key);
                foreach(var b in s)
                    Console.WriteLine($"Supervisor:{b.Name} Empolyees:{b.Emloyees}");
            }
                
        }

        public static void Quest_6(EmployeeContext db) {
            var list = db.Departments.Select(d=>d.Name).Union(db.Departments.Select(d=>d.City));

            foreach(var l in list)
                Console.WriteLine(l);
        }

        public static void Quest_7(EmployeeContext db) {
            var dept = from d in db.Employees.Include(e => e.Department)
                       group d by d.DepartamentId into g      
                       where g.Average(e=>e.Salary)<g.Average(e=>e.Commission)
                       select new
                       {
                           g.Key,
                           Avg = g.Average(e=>e.Salary)
                       };
            foreach(var d in dept)
                Console.WriteLine(d.Key+":"+d.Avg);
        }

        public static void Quest_8(EmployeeContext db) {
            var checking = from emp in db.Employees
                           join sal in db.SalGrades on 1 equals 1
                           where emp.Salary >= sal.Losal && emp.Salary <= sal.Hisal
                           select new {
                               emp.Salary,
                               Grade = sal.SalGradeId
                           };

            foreach(var x in checking)
            {
                Console.WriteLine(x.Salary+":"+x.Grade);
            }
        }

        public static void Quest_9(EmployeeContext db) {
            var employees = db.Employees.Include(e => e.Department).GroupBy(e => e.Department.City)
                .Select(e => new {
                    max=e.Max(d=>d.Salary),
                });

            
        }

        public static void Quest_10(EmployeeContext db) {
            var city_grade = db.Employees.Include(e => e.Department).Join(db.SalGrades,
                      e => 1,
                      s => 1,
                      (e, s) => new
                      {
                          e.Salary,
                          s.Hisal,
                          s.Losal,
                          s.SalGradeId,
                          e.Department.City
                      }).Where(e => e.Salary >= e.Losal && e.Salary <= e.Hisal)
                      .GroupBy(e=>new { e.City,e.SalGradeId})
                      .Select(e=>new {
                          e.Key.City,
                          e.Key.SalGradeId,
                          Employee=e.Count()
                      });

            foreach(var cg in city_grade)
                Console.WriteLine($"City:{cg.City} Grade:{cg.SalGradeId} Employee:{cg.Employee}");
        }

        private static bool CompareSalaries(int salary, List<int> salaryList)
        {
            foreach(int s in salaryList)
            {
                if (salary > s)
                    return true;   
            }
            return false;
        }

    }
}
