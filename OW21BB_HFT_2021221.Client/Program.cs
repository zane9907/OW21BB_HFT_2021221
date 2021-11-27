using OW21BB_HFT_2021221.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using ConsoleTools;
//using OW21BB_HFT_2021221.Logic;
//using OW21BB_HFT_2021221.Data;
//using OW21BB_HFT_2021221.Repository;

namespace OW21BB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            #region LOGICTEST
            //HospitalDbContext hpctx = new HospitalDbContext();


            //DoctorRepository doctorRepository = new DoctorRepository(hpctx);
            //PatientRepository patientRepository = new PatientRepository(hpctx);
            //HospitalRepository hospitalRepository = new HospitalRepository(hpctx);


            //DoctorLogic doctorLogic = new DoctorLogic(doctorRepository, patientRepository);
            //HospitalLogic hospitalLogic = new HospitalLogic(hospitalRepository, doctorRepository, patientRepository);
            //PatientLogic patientLogic = new PatientLogic(patientRepository);



            //var asd0 = doctorLogic.AllDiseasesPerDoctor();
            //var doctorNames = doctorRepository.GetAll().Select(x => x.Name).ToList();

            //int y = 0;
            //foreach (var item in asd0)
            //{
            //    Console.WriteLine($"DoctorName: {doctorNames[y++]}");
            //    Console.WriteLine("Diseases:");
            //    foreach (var dis in item)
            //    {
            //        Console.WriteLine($"-{dis}");
            //    }
            //    Console.WriteLine();
            //}




            //var asd1 = doctorLogic.AVGAgeOfDoctorsPatients();
            //var asd2 = hospitalLogic.DoctorSpecializatonCount();
            //var asd3 = doctorLogic.DiseasePerDoctor("Influenza");
            //var asd4 = hospitalLogic.PatientsPerHospital();
            //var asd5 = hospitalLogic.DoctorSpecializatonCountInSpecificHospital(1);

            //var asd6 = patientLogic.IsDiseasePresent("PTSD"); 
            #endregion

            Thread.Sleep(8000);

            RestService rs = new RestService("http://localhost:41147");

            var hospitals = rs.Get<Hospital>("hospital");

            var hospital = rs.GetSingle<Hospital>("hospital/1");


            var menu = new ConsoleMenu(args, level: 0)
        .Add("One", () => Yes(rs))
        .Add("Two", () => rs.GetSingle<Hospital>("hospital/1"))
        .Add("Exit", () => Environment.Exit(0))
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Main menu";
            config.EnableWriteTitle = true;
            config.EnableBreadcrumb = true;
        });

            menu.Show();




        }

        static void Yes(RestService rs)
        {
            Console.Clear();
            var hospitals = rs.Get<Hospital>("hospital");
            int i = 0;
            foreach (var item in hospitals)
            {
                Console.WriteLine($"[{i++}] - {item.Name}, {item.Location}");
            }


            Console.ReadLine();
        }
    }
}
