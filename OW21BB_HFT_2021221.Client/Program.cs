using OW21BB_HFT_2021221.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using ConsoleTools;
using System.Collections;
using System.Collections.Generic;
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

            #region THREADSLEEP AND LOADING
            Console.Write("L");
            Thread.Sleep(2000);
            Console.Write("O");
            Thread.Sleep(3000);
            Console.Write("A");
            Thread.Sleep(1000);
            Console.Write("D");
            Thread.Sleep(1000);
            Console.Write("I");
            Thread.Sleep(500);
            Console.Write("N");
            Thread.Sleep(500);
            Console.Write("G");
            Thread.Sleep(1500);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(600);
            Console.Write(".");
            Thread.Sleep(200); 
            #endregion


            RestService rs = new RestService("http://localhost:41147");
            
            #region GetALLMenu
            var subGetAll = new ConsoleMenu(args, level: 1)
           .Add("Get all hospitals", () => GetAllInstance(rs, "hospital"))
           .Add("Get all doctors", () => GetAllInstance(rs, "doctor"))
           .Add("Get all patients", () => GetAllInstance(rs, "patient"))
           .Add("Back", ConsoleMenu.Close)
           .Configure(config =>
           {
               config.Selector = "~>";
               config.EnableFilter = true;
               config.Title = "Get all data\n";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
           });
            #endregion

            #region GetOneMenu
            var subGetOne = new ConsoleMenu(args, level: 1)
        .Add("Get one hospital", () => GetOneInstance(rs, "hospital"))
        .Add("Get one doctor", () => GetOneInstance(rs, "doctor"))
        .Add("Get one patient", () => GetOneInstance(rs, "patient"))
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "~>";
            config.EnableFilter = true;
            config.Title = "Get all data\n";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
        });
            #endregion

            #region UPDATE
            var subUpdate = new ConsoleMenu(args, level: 1)
         .Add("Update a hospital", () => Update(rs, "hospital"))
         .Add("Update a doctor", () => Update(rs, "doctor"))
         .Add("Update a patient", () => Update(rs, "patient"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Update\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion

            #region DELETE
            var subDelete = new ConsoleMenu(args, level: 1)
         .Add("Delete a hospital", () => Delete(rs, "hospital"))
         .Add("Delete a doctor", () => Delete(rs, "doctor"))
         .Add("Delete a patient", () => Delete(rs, "patient"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Delete\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion

            #region CREATE
            var subCreate = new ConsoleMenu(args, level: 1)
         .Add("Create a hospital", () => Create(rs, "hospital"))
         .Add("Create a doctor", () => Create(rs, "doctor"))
         .Add("Create a patient", () => Create(rs, "patient"))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Create\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion

            #region STATS
            var subStats = new ConsoleMenu(args, level: 1)
         .Add("Avarage age of patients per doctor", () => AVGAgeOfDoctorsPatients(rs))
         .Add("Count of doctors specialization", () => DoctorSpecializatonCount(rs))
         .Add("Specific disease present at doctors", () => DiseasePerDoctor(rs))
         .Add("All patients per hospital", () => PatientsPerHospital(rs))
         .Add("Count of doctors specialization in specific hospital", () => DoctorSpecializatonCountInSpecificHospital(rs))
         .Add("Is the specific disease is present", () => IsDiseasePresent(rs))
         .Add("All diseases per doctor", () => AllDiseasesPerDoctor(rs))
         .Add("Back", ConsoleMenu.Close)
         .Configure(config =>
         {
             config.Selector = "~>";
             config.EnableFilter = true;
             config.Title = "Stats\n";
             config.EnableBreadcrumb = true;
             config.WriteBreadcrumbAction = titles => Console.Write(string.Join(" > ", titles));
         });
            #endregion


            #region MainMenu
            var menu = new ConsoleMenu(args, level: 0)
           .Add("Get all data", subGetAll.Show)
           .Add("Get one instance", subGetOne.Show)
           .Add("Create", subCreate.Show)
           .Add("Update", subUpdate.Show)
           .Add("Delete", subDelete.Show)
           .Add("Stats", subStats.Show)
           .Add("Exit", () => Environment.Exit(0))
           .Configure(config =>
           {
               config.Selector = "~>";
               config.EnableFilter = true;
               config.Title = "HOSPITAL DATABASE";
               config.EnableWriteTitle = false;
               config.EnableBreadcrumb = true;
           });
            #endregion

            menu.Show();
        }

        static void AllDiseasesPerDoctor(RestService rs)
        {
            Console.Clear();


            var alldisease = rs.GetSingle<IEnumerable<IEnumerable<string>>>($"stat/alldiseasesperdoctor");
            var doctorNames = rs.Get<Doctor>("doctor").Select(x => x.Name).ToList();

            int y = 0;
            foreach (var item in alldisease)
            {
                Console.WriteLine($"DoctorName: {doctorNames[y++]}");
                Console.WriteLine("Diseases:");
                foreach (var dis in item)
                {
                    Console.WriteLine($"-{dis}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void IsDiseasePresent(RestService rs)
        {
            Console.Clear();

            Console.Write("Type in the name of a disease: ");
            string disease = Console.ReadLine();
            Console.Clear();

            var stat = rs.GetSingle<bool>($"stat/IsDiseasePresent/{disease}");
            if (stat)
            {
                Console.WriteLine($"{disease} is present!");
            }
            else
            {
                Console.WriteLine($"{disease} is not present!");
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void DoctorSpecializatonCountInSpecificHospital(RestService rs)
        {
            Console.Clear();

            rs.Get<Hospital>($"hospital").ForEach(x => Console.WriteLine($"[{x.HospitalID}] - {x.Name}"));


            Console.Write("Select an id: "); Console.WriteLine();
            int hospitalID = int.Parse(Console.ReadLine());
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>($"stat/doctorspecializatoncountinspecifichospital/{hospitalID}").ToList();
            Console.WriteLine("Specialization -> Count of doctors");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void PatientsPerHospital(RestService rs)
        {
            Console.Clear();

            
            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>($"stat/patientsperhospital").ToList();
            Console.WriteLine("Hospital name: -> Count of patients");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void DiseasePerDoctor(RestService rs)
        {
            Console.Clear();

            Console.Write("Type in the name of a disease: ");
            string disease = Console.ReadLine();
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>($"stat/diseaseperdoctor/{disease}").ToList();
            Console.WriteLine("Doctor name: -> Count of disease");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void DoctorSpecializatonCount(RestService rs)
        {
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, int>>>("stat/doctorspecializatoncount").ToList();
            Console.WriteLine("Specialization -> Count");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void AVGAgeOfDoctorsPatients(RestService rs)
        {
            Console.Clear();

            var stat = rs.GetSingle<IEnumerable<KeyValuePair<string, double>>>("stat/avgageofdoctorspatients").ToList();
            Console.WriteLine("Doctor name -> AVG Age of patients");
            foreach (var item in stat)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Create(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("CREATE");

            if (model == "hospital")
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Hospital Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Hospital Location: ");
                string location = Console.ReadLine(); Console.WriteLine();
                


                rs.Post<Hospital>(new Hospital() { Name = name, Location = location }, model);
            }
            else if (model == "doctor")
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Doctor Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Doctor Specialization: ");
                string spec = Console.ReadLine(); Console.WriteLine();
                

                rs.Get<Hospital>($"{model}").ForEach(x => Console.WriteLine($"[{x.HospitalID}] - {x.Name}"));
                Console.WriteLine("Which hospital do you want to add the new doctor? (select id)");
                int id = int.Parse(Console.ReadLine());

                

                rs.Post<Doctor>(new Doctor() { Name = name,HospitalID = id, Specialization = spec }, model);
            }
            else
            {
                Console.WriteLine("Must fill in the following options:");
                Console.Write("Patient Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Age: ");
                string age = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Address: ");
                string address = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Disease: ");
                string disease = Console.ReadLine(); Console.WriteLine();

                rs.Get<Doctor>($"{model}").ForEach(x => Console.WriteLine($"[{x.DoctorID}] - {x.Name}"));
                Console.WriteLine("Which doctor do you want to add the new patient? (select id)");
                int id = int.Parse(Console.ReadLine());

                


                rs.Post<Patient>(new Patient() { Name = name, DoctorID = id, Age = int.Parse(age), Address = address, Disease = disease }, model);
            }

            Console.Clear();
            Console.WriteLine("Item created!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Delete(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("DELETE");
            if (model == "hospital")
            {
                rs.Get<Hospital>($"{model}").ForEach(x => Console.WriteLine($"[{x.HospitalID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);

            }
            else if (model == "doctor")
            {
                rs.Get<Doctor>($"{model}").ForEach(x => Console.WriteLine($"[{x.DoctorID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);
            }
            else
            {
                rs.Get<Patient>($"{model}").ForEach(x => Console.WriteLine($"[{x.PatientID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();

                rs.Delete(id, model);
            }
            Console.Clear();
            Console.WriteLine("Item deleted!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void Update(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("UPDATE");

            if (model == "hospital")
            {
                rs.Get<Hospital>($"{model}").ForEach(x => Console.WriteLine($"[{x.HospitalID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Hospital Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Hospital Location: ");
                string location = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Hospital>(new Hospital() { HospitalID = id, Name = name, Location = location }, model);
            }
            else if (model == "doctor")
            {
                rs.Get<Doctor>($"{model}").ForEach(x => Console.WriteLine($"[{x.DoctorID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Doctor Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Doctor Specialization: ");
                string spec = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Doctor>(new Doctor() { DoctorID = id, Name = name, Specialization = spec }, model);
            }
            else
            {
                rs.Get<Patient>($"{model}").ForEach(x => Console.WriteLine($"[{x.PatientID}] - {x.Name}"));

                Console.Write("Select an id: "); Console.WriteLine();
                int id = int.Parse(Console.ReadLine());
                Console.Clear();


                Console.WriteLine("Must fill in the following options:");
                Console.Write("Patient Name: ");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Age: ");
                string age = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Address: ");
                string address = Console.ReadLine(); Console.WriteLine();

                Console.Write("Patient Disease: ");
                string disease = Console.ReadLine(); Console.WriteLine();
                Console.Clear();


                rs.Put<Patient>(new Patient() { PatientID = id, Name = name, Age = int.Parse(age), Address = address, Disease = disease }, model);
            }

            Console.Clear();
            Console.WriteLine("Item updated!");
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void GetOneInstance(RestService rs, string model)
        {
            Console.Clear();
            Console.WriteLine("GET ONE");

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
            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }

        static void GetAllInstance(RestService rs, string model)
        {

            Console.Clear();
            Console.WriteLine("GET ALL");

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


            Console.WriteLine("\nPRESS ENTER TO CONTINUE");
            Console.ReadLine();
        }
    }
}
