using System;
using System.Linq;
using System.Reflection;
using OW21BB_HFT_2021221.Logic;
using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Repository;

namespace OW21BB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalDbContext hpctx = new HospitalDbContext();
            

            DoctorRepository doctorRepository = new DoctorRepository(hpctx);
            PatientRepository patientRepository = new PatientRepository(hpctx);
            HospitalRepository hospitalRepository = new HospitalRepository(hpctx);


            DoctorLogic doctorLogic = new DoctorLogic(doctorRepository, patientRepository);
            HospitalLogic hospitalLogic = new HospitalLogic(hospitalRepository, doctorRepository, patientRepository);


            var asd0 = doctorLogic.AllDiseasesPerDoctor();
            var doctorNames = doctorRepository.GetAll().Select(x => x.Name).ToList();

            int y = 0;
            foreach (var item in asd0)
            {                
                Console.WriteLine($"DoctorName: {doctorNames[y++]}");
                Console.WriteLine("Diseases:");
                foreach (var dis in item)
                {
                    Console.WriteLine($"-{dis}");
                }
                Console.WriteLine();
            }


            //var asd1 = doctorLogic.AVGAgeOfDoctorsPatients();
            //var asd2 = hospitalLogic.DoctorSpecializatonCount();
            //var asd3 = doctorLogic.DiseasePerDoctor("Influenza");
            //var asd4 = hospitalLogic.PatientsPerHospital();
            //var asd5 = hospitalLogic.DoctorSpecializatonCountInSpecificHospital(2);
            ;
        }
    }
}
