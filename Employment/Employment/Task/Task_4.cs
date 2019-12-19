using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employment.Task
{
    public class Task_4
    {
        public static void Quest_1(EmployeeContext db)
        {
            var empolyee = db.Employees.Where(e => e.Salary == (db.Employees.Min(x => x.Salary)));

            foreach(var e in empolyee)
                Console.WriteLine($"Empolyee:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_2(EmployeeContext db)
        {
            var employee = db.Employees
                .Where(e => e.Job.Equals(db.Employees
                                            .Where(x=>x.Name.Equals("BLAKE"))
                                            .Select(x=>x.Job).FirstOrDefault()));

            foreach (var e in employee)
                Console.WriteLine($"Employee:{e.Name} Job:{e.Job}");
        }

        public static void Quest_3(EmployeeContext db)
        {
            var salaries = db.Employees.GroupBy(e => e.DepartamentId)
                .Select(e => e.Min(x=>x.Salary));
            
            var people = db.Employees.Where(e => salaries.Contains(e.Salary));
            foreach(var p in people)
                Console.WriteLine($"Employee:{p.Name} Salary:{p.Salary}");
        }

        public static void Quest_4(EmployeeContext db)
        {
            var min_salaries = db.Employees.GroupBy(d => d.DepartamentId)
                .Select(s => new Model<int, int> {
                                Arg = s.Min(x => x.Salary),
                                Key = s.Key
                                 }).ToList();

            var employee = db.Employees
                .Where(e => min_salaries.Any(x => x.CompareTo(new Model<int, int>
                {
                    Key=e.DepartamentId,
                    Arg=e.Salary
                })));

            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
            
        }

        public static void Quest_5(EmployeeContext db)
       {
        var salaries = db.Employees
            .Where(e=>e.DepartamentId==30)
            .Select(e=>new { e.Salary });
        var empolyee = db.Employees.Where(e=> salaries.Any(x=>x.Salary<e.Salary));

        foreach(var s in salaries)
            Console.WriteLine(s);

        foreach (var e in empolyee)
            Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
       }

        public static void Quest_6(EmployeeContext db)
        {
            int max_salary = db.Employees.Where(e => e.DepartamentId == 30).Max(x=>x.Salary);
            var employee = db.Employees.Where(e => e.Salary > max_salary);
            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_7(EmployeeContext db)
        {
            var avr_salary = db.Employees.Where(e=>e.DepartamentId==30).Average(x => x.Salary);
            var departments = db.Employees.GroupBy(x => x.DepartamentId).Where(e => e.Average(x => x.Salary) > avr_salary);
            foreach(var d in departments)
                Console.WriteLine(d.Key);
        }

        public static void Quest_8(EmployeeContext db)
        {
            double avr_max_salary = db.Employees.GroupBy(e => e.Job)
                .Select(e => new {
                    salary=e.Average(x=>x.Salary)
                }).Max(x=>x.salary);

            var job = db.Employees.GroupBy(e => e.Job)
                .Select(e=>new {
                    e.Key,
                    Avr_Salary=e.Average(x=>x.Salary)
                })
                .Where(x => x.Avr_Salary==avr_max_salary);

            foreach(var j in job)
                Console.WriteLine($"Job:{j.Key} Avr_Salary:{j.Avr_Salary}");
        }

        public static void Quest_9(EmployeeContext db)
        {
            int max_salary = db.Employees.Include(x => x.Department)
                            .Where(e => e.Department.Name.Equals("SALES"))
                            .Max(s=>s.Salary);

            var employee = db.Employees.Where(e => e.Salary > max_salary);

            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_10(EmployeeContext db)
        {
            var employee = db.Employees.Where(e => e.Salary > db.Employees
                                                .Where(x => x.DepartamentId == e.DepartamentId)
                                                .Average(x => x.Salary));

            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_11(EmployeeContext db)
        {
            var managers = db.Employees.Select(e=>e.Manager);

            var employee = db.Employees.Where(e=>managers.Contains(e.EmployeeId));
            foreach(var e in employee)
                Console.WriteLine($"Manager:{e.EmployeeId}");
        }

        public static void Quest_12(EmployeeContext db)
        {
            var departments = db.Departments.Select(d => d.DepartmentId);

            var department = db.Employees.Where(e => !departments.Contains(e.DepartamentId));

            foreach(var d in department)
                Console.WriteLine($"Deparment:{d.Department.Name}");
        }
        
        public static void Quest_13(EmployeeContext db)
        {
            var jobs = db.Employees.GroupBy(x => x.Job)
                .Select(x => new Model<string,int>{
                    Key=x.Key,
                    Arg = x.Max(s => s.Salary)
                }).ToList();

            var empolyee = db.Employees
                .Where(e=>jobs.Any(x=>x.CompareTo(new Model<string,int> { Key=e.Job, Arg=e.Salary})));

            foreach(var e in empolyee)
                Console.WriteLine(e.Name+" "+e.Job+"  "+e.Salary);
        }

        public static void Quest_14(EmployeeContext db)
        {
            var min_salaries = db.Employees.GroupBy(d => d.DepartamentId)
                .Select(x=>new Model<int,int> {
                    Key=x.Key,
                    Arg=x.Min(s=>s.Salary)
                }).ToList();
            
            var employee = db.Employees.Where(e => min_salaries
                                            .Any(x => x.CompareTo(new Model<int, int> { Key=e.DepartamentId, Arg=e.Salary })))
                                            .OrderBy(x=>x.Name);
            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Department:{e.DepartamentId} Salary:{e.Salary}");
        }

        public static void Quest_15(EmployeeContext db)
        {
            var hiredates = db.Employees.GroupBy(d => d.DepartamentId)
                .Select(e => new Model<int, DateTime> {
                    Key = e.Key,
                    Arg = e.Max(x => x.HireDate)
                }).ToList();

            var employee = db.Employees.Where(e => hiredates
                                    .Any(x => x.CompareTo(new Model<int, DateTime> {
                                        Key=e.DepartamentId,
                                        Arg=e.HireDate
                                     }))).OrderBy(x=>x.HireDate);

            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Department:{e.DepartamentId} Hiredate:{e.HireDate}");
        }

        public static void Quest_16(EmployeeContext db)
        {
            var avr_salary = db.Employees.GroupBy(d => d.DepartamentId)
                .Select(e=>new Model<int,double> {
                    Key = e.Key,
                    Arg = e.Average(x=>x.Salary) }).ToList();

            var info = db.Employees.Where(e=>avr_salary.Any(x=>x.Bigger(new Model<int, double> {
                                                                            Key = e.DepartamentId,
                                                                            Arg = e.Salary})));
            foreach(var i in info)
                Console.WriteLine($"Employee:{i.Name} Salary:{i.Salary} Department:{i.DepartamentId}");
        }

        public static void Quest_17(EmployeeContext db)
        {
            var dept = db.Employees.Select(e=>e.DepartamentId).Distinct().ToList();

            var departments = db.Departments.Where(x=>!dept.Any(d=>d.Equals(x.DepartmentId)));
            
            foreach(var dd in departments)
                Console.WriteLine($"Department:{dd.DepartmentId}");
        }

        public static void Quest_18(EmployeeContext db)
        {
            var employee = db.Employees.OrderByDescending(e => e.Salary).Take(3);
            foreach(var e in employee)
                Console.WriteLine($"Employee:{e.Name} Salary:{e.Salary}");
        }

        public static void Quest_19(EmployeeContext db)
        {
            var max_count = db.Employees.GroupBy(d => d.DepartamentId).Max(x=>x.Count());
            var department = db.Employees.GroupBy(d=>d.DepartamentId).Where(d=>d.Count()==max_count);
            foreach(var d in department)
                Console.WriteLine($"Department:{d.Key} Count:{d.Count()}");
        }

        
    }
}
