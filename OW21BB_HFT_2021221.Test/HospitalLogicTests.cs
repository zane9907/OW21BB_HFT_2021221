using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Repository;
using OW21BB_HFT_2021221.Logic;
using OW21BB_HFT_2021221.Models;

namespace OW21BB_HFT_2021221.Test
{
    [TestFixture]
    public class HospitalLogicTests
    {
        private HospitalLogic HospitalLogic { get; set; }
        private DoctorLogic DoctorLogic { get; set; }
        private PatientLogic PatientLogic { get; set; }
        
        [SetUp]
        public void Setup()
        {
            Mock<HospitalRepository> mockedHospRepo = new Mock<HospitalRepository>();
            Mock<DoctorRepository> mockedDocRepo = new Mock<DoctorRepository>();
            Mock<PatientRepository> mockedPatRepo = new Mock<PatientRepository>();

            mockedHospRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new Hospital()
                {
                    HospitalID = 2,
                    Name = "Hospital2",
                    Location = "New York"
                });

            mockedHospRepo.Setup(x => x.GetAll()).Returns(this.FakeHospitalObjects);
            mockedDocRepo.Setup(x => x.GetAll()).Returns(this.FakeDoctorObjects);
            mockedPatRepo.Setup(x => x.GetAll()).Returns(this.FakePatientObjects);

            this.HospitalLogic = new HospitalLogic(mockedHospRepo.Object, mockedDocRepo.Object, mockedPatRepo.Object);
            this.DoctorLogic = new DoctorLogic( mockedDocRepo.Object, mockedPatRepo.Object);
            this.PatientLogic = new PatientLogic( mockedPatRepo.Object);

        }

        private IQueryable<Hospital> FakeHospitalObjects()
        {
            #region Data
            Hospital h0 = new Hospital() { HospitalID = 1, Name = "Hospital1", Location = "Budapest" };


            Doctor d0 = new Doctor() { DoctorID = 1, Name = "John", Specialization = "Cardiology" };
            Doctor d1 = new Doctor() { DoctorID = 2, Name = "Mark", Specialization = "Neurology" };
            Doctor d2 = new Doctor() { DoctorID = 3, Name = "Mark", Specialization = "Neurology" };

            Patient p0 = new Patient() { PatientID = 1, Name = "Elizabeth", Age = 18, Address = "420, Somwhere, 69 IDontKnow street", Disease = "AIDS" };
            Patient p1 = new Patient() { PatientID = 2, Name = "Ann", Age = 8, Address = "8594, Richmond, 3843 Coulter Lane.", Disease = "Influenza" };
            Patient p2 = new Patient() { PatientID = 3, Name = "Michael", Age = 38, Address = "59353, Wibaux, 1837 Tibbs Avenue", Disease = "Malaria" };
            Patient p3 = new Patient() { PatientID = 4, Name = "Josh", Age = 99, Address = "35401, Alabama, 5001 Brookside Drive", Disease = "Meningitis" };
            Patient p4 = new Patient() { PatientID = 5, Name = "Adam", Age = 25, Address = "3532, 1374 Gerald L. Bates Drive", Disease = "AIDS" };

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
            #endregion

            List<Hospital> items = new List<Hospital>();

            items.Add(h0);
            items.Add(h1);

            return items.AsQueryable();
        }

        private IQueryable<Doctor> FakeDoctorObjects()
        {
            #region Data
            Hospital h0 = new Hospital() { HospitalID = 1, Name = "Hospital1", Location = "Budapest" };


            Doctor d0 = new Doctor() { DoctorID = 1, Name = "John", Specialization = "Cardiology" };
            Doctor d1 = new Doctor() { DoctorID = 2, Name = "Mark", Specialization = "Neurology" };
            Doctor d2 = new Doctor() { DoctorID = 3, Name = "Mark", Specialization = "Neurology" };

            Patient p0 = new Patient() { PatientID = 1, Name = "Elizabeth", Age = 18, Address = "420, Somwhere, 69 IDontKnow street", Disease = "AIDS" };
            Patient p1 = new Patient() { PatientID = 2, Name = "Ann", Age = 8, Address = "8594, Richmond, 3843 Coulter Lane.", Disease = "Influenza" };
            Patient p2 = new Patient() { PatientID = 3, Name = "Michael", Age = 38, Address = "59353, Wibaux, 1837 Tibbs Avenue", Disease = "Malaria" };
            Patient p3 = new Patient() { PatientID = 4, Name = "Josh", Age = 99, Address = "35401, Alabama, 5001 Brookside Drive", Disease = "Meningitis" };
            Patient p4 = new Patient() { PatientID = 5, Name = "Adam", Age = 25, Address = "3532, 1374 Gerald L. Bates Drive", Disease = "AIDS" };

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
            #endregion

            List<Doctor> items = new List<Doctor>();

            items.Add(d0);
            items.Add(d1);
            items.Add(d2);
            items.Add(d3);
            items.Add(d4);

            return items.AsQueryable();
        }

        private IQueryable<Patient> FakePatientObjects()
        {
            #region Data
            Hospital h0 = new Hospital() { HospitalID = 1, Name = "Hospital1", Location = "Budapest" };


            Doctor d0 = new Doctor() { DoctorID = 1, Name = "John", Specialization = "Cardiology" };
            Doctor d1 = new Doctor() { DoctorID = 2, Name = "Mark", Specialization = "Neurology" };
            Doctor d2 = new Doctor() { DoctorID = 3, Name = "Mark", Specialization = "Neurology" };

            Patient p0 = new Patient() { PatientID = 1, Name = "Elizabeth", Age = 18, Address = "420, Somwhere, 69 IDontKnow street", Disease = "AIDS" };
            Patient p1 = new Patient() { PatientID = 2, Name = "Ann", Age = 8, Address = "8594, Richmond, 3843 Coulter Lane.", Disease = "Influenza" };
            Patient p2 = new Patient() { PatientID = 3, Name = "Michael", Age = 38, Address = "59353, Wibaux, 1837 Tibbs Avenue", Disease = "Malaria" };
            Patient p3 = new Patient() { PatientID = 4, Name = "Josh", Age = 99, Address = "35401, Alabama, 5001 Brookside Drive", Disease = "Meningitis" };
            Patient p4 = new Patient() { PatientID = 5, Name = "Adam", Age = 25, Address = "3532, 1374 Gerald L. Bates Drive", Disease = "AIDS" };

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
            #endregion

            List<Patient> items = new List<Patient>();

            items.Add(p0);
            items.Add(p1);
            items.Add(p2);
            items.Add(p3);
            items.Add(p4);
            items.Add(p5);
            items.Add(p6);
            items.Add(p7);
            items.Add(p8);
            

            return items.AsQueryable();
        }
    }
}
