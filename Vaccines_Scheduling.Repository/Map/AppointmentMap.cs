using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository.Map
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("tb_agendamento");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id_agendamento")
                   .IsRequired();

            builder.Property(e => e.IdPatient)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(e => e.Date)
                   .HasColumnName("dat_agendamento")
                   .HasConversion(
                    v => v.ToDateTime(new TimeOnly(0)), 
                    v => DateOnly.FromDateTime(v))
                   .IsRequired();

            builder.Property(e => e.Time)
                   .HasColumnName("hor_agendamento")
                   .HasConversion(
                    v => v.ToTimeSpan(),               
                    v => TimeOnly.FromTimeSpan(v))
                   .HasMaxLength(7)
                   .IsRequired();

            builder.Property(e => e.Status)
                   .HasColumnName("dsc_status")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.CreationTime)
                   .HasColumnName("dat_criacao")
                   .IsRequired();
        }       
    }
}
