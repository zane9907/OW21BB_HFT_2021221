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

            var subGetAll = new ConsoleMenu(args, level: 1)
        .Add("Get all hospitals", () => GetAllInstance(rs, "hospital"))
        .Add("Get all doctors", () => GetAllInstance(rs, "doctor"))
        .Add("Get all patients", () => GetAllInstance(rs, "patient"))
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Get all data";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });


            var subGetOne = new ConsoleMenu(args, level: 1)
        .Add("Get one hospital", () => GetOneInstance(rs, "hospital"))
        .Add("Get one doctor", () => GetOneInstance(rs, "doctor"))
        .Add("Get one patient", () => GetOneInstance(rs, "patient"))
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Get all data";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var menu = new ConsoleMenu(args, level: 0)
        .Add("Get all data", subGetAll.Show)
        .Add("Get one instance", subGetOne.Show)
        .Add("Exit", () => Environment.Exit(0))
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "HOSPITAL DATABASE\n------------------";
            config.EnableWriteTitle = true;
            config.EnableBreadcrumb = true;
        });



            menu.Show();
        }
        static void GetOneInstance(RestService rs, string model)
        {
            Console.Clear();

            if (model == "hospital")
            {
                rs.Get<Hospital>($"{model}").ForEach(x => Console.WriteLine($"[{x.HospitalID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var hos = rs.GetSingle<Hospital>($"{model}/{id}");
                Console.WriteLine($"{hos.AllData} - Doctors: {hos.DoctorCount}");

            }
            else if (model == "doctor")
            {
                rs.Get<Doctor>($"{model}").ForEach(x => Console.WriteLine($"[{x.DoctorID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var doc = rs.GetSingle<Doctor>($"{model}/{id}");
                Console.WriteLine($"{doc.AllData} - Patients: {doc.PatientCount}");
            }
            else
            {
                rs.Get<Patient>($"{model}").ForEach(x => Console.WriteLine($"[{x.PatientID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                var pat = rs.GetSingle<Patient>($"{model}/{id}");
                Console.WriteLine($"{pat.AllData}");
            }

            Console.ReadLine();
        }

        static void GetAllInstance(RestService rs, string model)
        {

            Console.Clear();
            if (model == "hospital")
            {
                rs.Get<Hospital>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }
            else if (model == "doctor")
            {
                rs.Get<Doctor>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }
            else
            {
                rs.Get<Patient>($"{model}").ForEach(x => Console.WriteLine(x.AllData));
            }

            Console.ReadLine();    
        }
    }
}
