using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagementSystem.Entities;

namespace UniversityManagementSystem.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students_Master");

            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Name)
                   .HasColumnName("Student_FullName")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Email).IsRequired();
            builder.HasIndex(s => s.Email).IsUnique();

            builder.Property(s => s.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.IsActive)
                   .HasDefaultValue(true);

            builder.Ignore(s => s.TempCalculation);

            builder.HasIndex(s => new { s.Name, s.BranchId });

            builder.HasOne(s => s.Address)
                   .WithOne(a => a.Student)
                   .HasForeignKey<Address>(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Course)
                   .WithMany(c => c.Students)
                   .HasForeignKey(s => s.CourseId);
        }
    }

}
