using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository.Map
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("tb_paciente");

            builder.Property(e => e.Id)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasColumnName("dsc_nome")
                   .IsRequired();

            builder.Property(e => e.Login)
                   .HasColumnName("lgn_usuario")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Birthday)
                   .HasColumnName("dat_nascimento")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.CreationTime)
                   .HasColumnName("dat_criacao")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.PasswordHash)
                  .HasColumnName("psw_hash")
                  .IsRequired();

            builder.Property(e => e.PasswordSalt)
                  .HasColumnName("psw_salt")
                  .IsRequired();
        }
    }
}
