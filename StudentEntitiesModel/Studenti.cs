namespace StudentModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Studenti")]
    public partial class Student
    {
        [Key]
        public int NumarMatricol { get; set; }

        [Required]
        [StringLength(20)]
        public string Nume { get; set; }

        [Required]
        [StringLength(20)]
        public string Prenume { get; set; }

        [Required]
        [StringLength(2)]
        public string FormaInvatamant { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }
    }
}
