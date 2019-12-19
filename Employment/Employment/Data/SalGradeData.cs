using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employment
{
    public static class SalGradeData
    {
        public static void Initialize(this EntityTypeBuilder<SalGrade> builder)
        {
            builder.HasData(
                new SalGrade[]{
                new SalGrade{SalGradeId=1,Losal=700,Hisal=1200 },
                new SalGrade{SalGradeId=2,Losal=1201,Hisal=1400 },
                new SalGrade{SalGradeId=3,Losal=1401,Hisal=2000 },
                new SalGrade{SalGradeId=4,Losal=2001,Hisal=3000 },
                new SalGrade{SalGradeId=5,Losal=3001,Hisal=9999 }
            });
        }
    }
}
