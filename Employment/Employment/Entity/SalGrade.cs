using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Employment
{
    public class SalGrade
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GradeId")]
        public int SalGradeId { get; set;}
        [Required]
        public int Losal { get; set; }
        [Required]
        public int Hisal { get; set; }
    }
}
