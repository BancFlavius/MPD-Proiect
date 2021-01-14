namespace StudentModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public  class StudentEntitiesModel : DbContext
    {
        public StudentEntitiesModel()
            : base("name=StudentEntitiesModel")
        {
        }

        public virtual DbSet<Student> Studenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(e => e.Nume)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.Prenume)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.FormaInvatamant)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .Property(e => e.Email)
                .IsFixedLength();
        }
    }
}
