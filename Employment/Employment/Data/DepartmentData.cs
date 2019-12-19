using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employment
{
    public static class DepartmentData
    {
        public static void Initialize(this EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department[]
                {
                    new Department{ DepartmentId=10,Name="ACCOUNTING",City="NEW YORK"},
                    new Department{ DepartmentId=20,Name="RESEARCH",City="DALLAS"},
                    new Department{ DepartmentId=30,Name="SALES",City="CHICAGO"},
                    new Department{ DepartmentId=40,Name="OPERATIONS",City="BOSTON"},
                }
                );
                
        }
    }
}
