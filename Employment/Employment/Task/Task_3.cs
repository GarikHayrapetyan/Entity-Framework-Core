using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Employment.Task
{
    public class Task_3
    {
        public static void Quest_1(EmployeeContext db)
        {
            var avgSalary = db.Employees.Select(e => e.Salary).Average();
            Console.WriteLine($"Average salary:{avgSalary}");
        }

        public static void Quest_2(EmployeeContext db)
        {
            int lowestSalary = db.Employees.Where(e => e.Job == "CLERK").Select(e => e.Salary).Min();
            Console.WriteLine($"Lowest Salary of Clerk:{lowestSalary}");
        }

        public static void Quest_3(EmployeeContext db)
        {
            int people = db.Employees.Where(e => e.DepartamentId == 20).Count();
            Console.WriteLine($"Staffs at dept 20:{people}");
        }

        public static void Quest_4(EmployeeContext db)
        {
            var jobs = db.Employees.GroupBy(e => e.Job)
                .Select(e => new
                {
                    Job = e.Key,
                    Average_Salary = e.Average(j => j.Salary)
                });

            foreach (var j in jobs)
                Console.WriteLine($"Job:{j.Job} Average Salary:{j.Average_Salary}");
        }

        public static void Quest_5(EmployeeContext db)
        {
            var jobs = db.Employees.Where(e => !e.Job.Equals("MANAGER")).GroupBy(e => e.Job)
                .Select(e => new
                {
                    Job = e.Key,
                    Average_Salary = e.Average(j => j.Salary)
                });

            foreach (var j in jobs)
                Console.WriteLine($"Job:{j.Job} Average Salary:{j.Average_Salary}");
        }

        public static void Quest_6(EmployeeContext db)
        {
            var tt = db.Employees.GroupBy(e => new { e.DepartamentId, e.Job }).Select(e => new
            {
                Dept = e.Key.DepartamentId,
                e.Key.Job,
                Aver_Salary = e.Average(x => x.Salary),
                Emp = e.Count()

            }).OrderBy(x => x.Dept);

            foreach (var j in tt)
                Console.WriteLine($"Deptarment:{j.Dept} Job:{j.Job} Average_Salary:{j.Aver_Salary} Employee:{j.Emp}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var jobs = db.Employees.GroupBy(e => e.DepartamentId).Select(e => new
            {
                e.Key,
                inner_jobs = e.GroupBy(x => x.Job).Select(y => new
                {
                    y.Key,
                    Average_Salary = y.Average(i => i.Salary),
                    Employee = y.Count()
                })
            });

            foreach (var job in jobs)
            {
                Console.WriteLine($"Department:{job.Key}");
                foreach (var j in job.inner_jobs)
                    Console.WriteLine($"Job:{j.Key} " +
                        $"AveragerSalary:{j.Average_Salary} " +
                        $"Employee:{j.Employee}");
            }
            //foreach(var job in jobs)
            //{
            //    var each_job = job.GroupBy(e => e.Job)
            //        .Select(e=>new {
            //            e.Key,
            //            Avg_Salary =e.Average(x => x.Salary),
            //            Employee=e.Count()});
            //    foreach(var j in each_job)
            //        Console.WriteLine($"Department:{job.Key} Job:{j.Key} AverageSalary:{j.Avg_Salary} Employee:{j.Employee}");
            //    Console.WriteLine();
            //}
        }

        public static void Quest_7(EmployeeContext db)
        {
            var jobs = db.Employees.Select(j => new { j.Job, j.Salary }).GroupBy(j => j.Job).Select(j => new
            {
                j.Key,
                Max_Salary = j.Max(x => x.Salary)
            });

            foreach (var job in jobs)
                Console.WriteLine($"Job:{job.Key} Max_Salary:{job.Max_Salary}");
        }

        public static void Quest_8(EmployeeContext db)
        {
            var departments = db.Employees.Select(e => new
            {
                e.EmployeeId,
                e.DepartamentId,
                e.Salary
            }).GroupBy(e => e.DepartamentId).Select(d => new
            {
                d.Key,
                Average_Salary = d.Average(x => x.Salary),
                Employees = d.Count()
            }).Where(e => e.Employees > 3);

            foreach (var d in departments)
                Console.WriteLine($"Department:{d.Key} Employees:{d.Employees} Average_Salary:{d.Average_Salary}");
        }

        public static void Quest_9(EmployeeContext db)
        {
            var jobs = db.Employees.Select(e => new { e.Job, e.Salary })
                .GroupBy(e => e.Job)
                .Select(e => new
                {
                    e.Key,
                    Average_Salary = e.Average(x => x.Salary)
                }).Where(j => j.Average_Salary >= 3000);

            foreach (var j in jobs)
                Console.WriteLine($"Job:{j.Key} Average Salary:{j.Average_Salary}");
        }

        public static void Quest_10(EmployeeContext db)
        {
            var jobs = db.Employees.Select(e => new { e.Job, e.Salary, e.Commission })
                .GroupBy(e => e.Job)
                .Select(e => new
                {
                    e.Key,
                    Average_Monthly_Salary = e.Average(x => x.Salary),
                    Average_Yearly_Salary = e.Average(x => (x.Salary * 12 + (x.Commission ?? 0)))
                });
            foreach (var j in jobs)
                Console.WriteLine($"Job:{j.Key}  Average Monthly Salary:{j.Average_Monthly_Salary}" +
                    $" Average Yearly Salary:{j.Average_Yearly_Salary}");
        }

        public static void Quest_11(EmployeeContext db)
        {
            var difference = db.Employees.Max(x => x.Salary) - db.Employees.Min(x => x.Salary);
            Console.WriteLine(difference);
        }

        public static void Quest_12(EmployeeContext db)
        {
            var departments = db.Employees.GroupBy(e => e.DepartamentId).Where(e => e.Count() > 3);
            foreach (var d in departments)
                Console.WriteLine($"Department:{d.Key} Employees:{d.Count()}");
        }

        public static void Quest_13(EmployeeContext db)
        {
            var difference = db.Employees.Count() - db.Employees.Select(e => e.EmployeeId).Count();
            Console.WriteLine($"Difference:{difference}");
        }

        public static void Quest_14(EmployeeContext db)
        {
            var lowestSalaries = db.Employees.Join(db.Employees,
                e => e.Manager,
                b => b.EmployeeId,
                (e, b) => new
                {
                    e.Manager,
                    e.Salary,
                    b.Job,
                }).Where(e => e.Salary >= 1000)
                .GroupBy(e => e.Manager)
                .Select(b => new {
                    Min_Salary = b.Min(x => x.Salary),
                    b.Key
                }).OrderBy(x=>x.Min_Salary);

            foreach(var ls in lowestSalaries)
                Console.WriteLine($"BossID:{ls.Key} EmpSalary:{ls.Min_Salary}");
        }

        public static void Quest_15(EmployeeContext db) {
            int people = db.Employees.Include(e => e.Department)
                .Where(d => d.Department.City.Equals("DALLAS"))
                .Count();
            Console.WriteLine($"Employees in DALLAS:{people}");
        }

        public static void Quest_16(EmployeeContext db)
        {
            var es = db.Employees.Join(db.SalGrades,
                e=>1,
                s=>1,
                (e, s) => new {
                    e.Salary,
                    s.SalGradeId,
                    s.Losal,
                    s.Hisal
                }).Where(e=>e.Salary>e.Losal && e.Salary<e.Hisal)
                .GroupBy(e=>e.SalGradeId)
                .Select(e=>new {
                    e.Key,
                    Max_Salary=e.Max(x=>x.Salary)
                });

            foreach(var a in es)
            {
                Console.WriteLine($"Salary:{a.Key} Grade:{a.Max_Salary}");
            }
               
        }

        public static void Quest_17(EmployeeContext db)
        {
            var salaries = db.Employees.GroupBy(e => e.Salary)
                .Where(x=>x.Count()>1)
                .Select(s=>new {
                    s.Key,
                    Salary=s.Count()
                });

            foreach(var s in salaries)
                Console.WriteLine($"Salary:{s.Key} Count:{s.Salary}");
        }

        public static void Quest_18(EmployeeContext db)
        {
            double avr_salary = db.Employees.Join(db.SalGrades,
                e => 1,
                s => 1,
                (e, s) => new
                {
                    e.Salary,
                    s.SalGradeId,
                    s.Losal,
                    s.Hisal
                }).Where(e=>e.Salary>e.Losal && e.Salary<e.Hisal && e.SalGradeId==2)
                .Average(x=>x.Salary);
            Console.WriteLine($"Average salary of Second Grade:{avr_salary}");
        }

        public static void Quest_19(EmployeeContext db)
        {
            var manager_staff = db.Employees.Join(db.Employees,
                e => e.Manager,
                b => b.EmployeeId,
                (e, b) => new
                {
                    e.Manager,
                    e.Salary,
                    b.Job,
                })
                .GroupBy(e => e.Manager)
                .Select(b => new {
                    Staff = b.Count(),
                    b.Key
                });

            foreach(var ms in manager_staff)
                Console.WriteLine($"Manager:{ms.Key} Staff:{ms.Staff}");
        }

        public static void Quest_20(EmployeeContext db) {
            int income = db.Employees.Join(db.SalGrades,
                e => 1,
                s => 1,
                (e, s) => new
                {
                    e.Commission,
                    e.Salary,
                    s.SalGradeId,
                    s.Losal,
                    s.Hisal
                }).Where(e => e.Salary > e.Losal && e.Salary < e.Hisal && e.SalGradeId == 1)
                .Sum(x=>x.Salary+(x.Commission??0));
            Console.WriteLine($"Income for First Grade:{income}");
                
        }
    }
}
