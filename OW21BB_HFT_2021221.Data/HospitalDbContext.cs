using Microsoft.EntityFrameworkCore;
using OW21BB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Data
{
    public class HospitalDbContext : DbContext
    {
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        public HospitalDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HospitalDatabase.mdf;Integrated Security=True");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(patient => patient.Doctor)
                .WithMany(doctor => doctor.Patients)
                .HasForeignKey(patient => patient.DoctorID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasOne(doctor => doctor.Hospital)
                .WithMany(hospital => hospital.Doctors)
                .HasForeignKey(doctor => doctor.HospitalID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            Hospital h0 = new Hospital() { HospitalID = 1, Name = "Hospital1", Location = "Budapest" };
            

            Doctor d0 = new Doctor() { DoctorID = 1, Name = "John", Specialization = "Cardiology" };
            Doctor d1 = new Doctor() { DoctorID = 2, Name = "Mark", Specialization = "Neurology" };
            Doctor d2 = new Doctor() { DoctorID = 3, Name = "Mark", Specialization = "Neurology" };

            Patient p0 = new Patient() { PatientID = 1, Name = "Elizabeth", Age = 18, Address = "420, Somwhere, 69 IDontKnow street", Disease = "AIDS" };
            Patient p1 = new Patient() { PatientID = 2, Name = "Ann", Age = 8, Address = "8594, Richmond, 3843 Coulter Lane.", Disease = "Influenza" };
            Patient p2 = new Patient() { PatientID = 3, Name = "Michael", Age = 38, Address = "59353, Wibaux, 1837 Tibbs Avenue", Disease = "Malaria" };
            Patient p3 = new Patient() { PatientID = 4, Name = "Josh", Age = 99, Address = "35401, Alabama, 5001 Brookside Drive", Disease= "Meningitis" };
            Patient p4 = new Patient() { PatientID = 5, Name = "Adam", Age = 25, Address = "3532, 1374 Gerald L. Bates Drive", Disease= "Common cold" };

            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            Hospital h1 = new Hospital() { HospitalID = 2, Name = "Hospital2", Location = "New York" };

            Doctor d3 = new Doctor() { DoctorID = 4, Name = "John", Specialization = "Cardiology" };
            Doctor d4 = new Doctor() { DoctorID = 5, Name = "Mark", Specialization = "Neurology" };

            Patient p5 = new Patient() { PatientID = 6, Name = "Elizabeth", Age = 46, Address = "420, Somwhere, 69 IDontKnow street", Disease = "AIDS" };
            Patient p6 = new Patient() { PatientID = 7, Name = "Ann", Age = 8, Address = "8594, Richmond, 3843 Coulter Lane.", Disease = "Influenza" };
            Patient p7 = new Patient() { PatientID = 8, Name = "Michael", Age = 38, Address = "59353, Wibaux, 1837 Tibbs Avenue", Disease = "Malaria" };
            Patient p8 = new Patient() { PatientID = 9, Name = "Josh", Age = 99, Address = "35401, Alabama, 5001 Brookside Drive", Disease = "Meningitis" };


            //h0.Doctors.Add(d0);
            //h0.Doctors.Add(d1);

            //d0.Patients.Add(p0);
            //d0.Patients.Add(p1);

            //d1.Patients.Add(p2);
            //d1.Patients.Add(p3);


            d0.HospitalID = h0.HospitalID;
            d1.HospitalID = h0.HospitalID;
            d2.HospitalID = h0.HospitalID;

            p0.DoctorID = d0.DoctorID;
            p1.DoctorID = d0.DoctorID;
            p4.DoctorID = d0.DoctorID;

            p2.DoctorID = d1.DoctorID;
            p3.DoctorID = d1.DoctorID;

            //-----------------------------------

            d3.HospitalID = h1.HospitalID;
            d4.HospitalID = h1.HospitalID;

            p5.DoctorID = d3.DoctorID;
            p6.DoctorID = d3.DoctorID;

            p7.DoctorID = d4.DoctorID;
            p8.DoctorID = d4.DoctorID;


            //-----------------------------------


            modelBuilder.Entity<Hospital>().HasData(h0, h1);
            modelBuilder.Entity<Doctor>().HasData(d0, d1, d2, d3, d4);
            modelBuilder.Entity<Patient>().HasData(p0, p1, p2, p3, p4, p5, p6, p7, p8);

            
        }
    }
}
