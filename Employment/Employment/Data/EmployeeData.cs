using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employment
{
   public static class EmployeeData
    {
        public static void Initialize(this EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee[]
                {
                    new Employee{EmployeeId=7839,Name="KING",Job="PRESIDENT",HireDate=new DateTime(1981,11,17).Date,Salary=5000,DepartamentId=10 },
                    new Employee{EmployeeId=7698,Name="BLAKE",Job="MANAGER",Manager=7839,HireDate=new DateTime(1981,5,1).Date,Salary=2850,DepartamentId=30 },
                    new Employee{EmployeeId=7782,Name="CLARK",Job="MANAGER",Manager=7839,HireDate=new DateTime(1981,6,9).Date,Salary=2450,DepartamentId=10 },
                    new Employee{EmployeeId=7566,Name="JONES",Job="MANAGER",Manager=7839,HireDate=new DateTime(1981,4,2).Date,Salary=2975,DepartamentId=20 },
                    new Employee{EmployeeId=7654,Name="MARTIN",Job="SALESMAN",Manager=7698,HireDate=new DateTime(1981,9,28).Date,Salary=1250,Commission=1400,DepartamentId=30 },
                    new Employee{EmployeeId=7499,Name="ALLEN",Job="SALESMAN",Manager=7698,HireDate=new DateTime(1981,2,20).Date,Salary=1600,Commission=300,DepartamentId=30 },
                    new Employee{EmployeeId=7844,Name="TURNER",Job="SALESMAN",Manager=7698,HireDate=new DateTime(1981,9,8).Date,Salary=1500,Commission=0,DepartamentId=30 },
                    new Employee{EmployeeId=7900,Name="JAMES",Job="CLERK",Manager=7698,HireDate=new DateTime(1981,12,3).Date,Salary=950,DepartamentId=30 },
                    new Employee{EmployeeId=7521,Name="WARD",Job="SALESMAN",Manager=7698,HireDate=new DateTime(1981,2,22).Date,Salary=1250,Commission=500,DepartamentId=30 },
                    new Employee{EmployeeId=7902,Name="FORD",Job="ANALYST",Manager=7566,HireDate=new DateTime(1981,12,3).Date,Salary=3000,DepartamentId=20 },
                    new Employee{EmployeeId=7369,Name="SMITH",Job="CLERK",Manager=7902,HireDate=new DateTime(1980,12,17).Date,Salary=800,DepartamentId=20 },
                    new Employee{EmployeeId=7788,Name="SCOTT",Job="ANALYST",Manager=7566,HireDate=new DateTime(1982,12,9).Date,Salary=3000,DepartamentId=20 },
                    new Employee{EmployeeId=7876,Name="ADAMS",Job="CLERK",Manager=7788,HireDate=new DateTime(1983,1,12).Date,Salary=1100,DepartamentId=20 },
                    new Employee{EmployeeId=7934,Name="MILLER",Job="CLERK",Manager=7782,HireDate=new DateTime(1982,1,23).Date,Salary=1300,DepartamentId=10 }
                }
                );
        }
    }
}
