using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Logic
{
    public class HospitalLogic : IHospitalLogic
    {
        IRepository<Hospital> hospitalRepo;
        IRepository<Doctor> doctorRepository;
        IRepository<Patient> patientRepository;

        public HospitalLogic(IRepository<Hospital> hospitalRepo, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
        {
            this.hospitalRepo = hospitalRepo;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }

        public void AddNewHospital(Hospital hospital)
        {
            hospitalRepo.Create(hospital);
        }

        public void DeleteHospital(Hospital t)
        {
            hospitalRepo.Delete(t);
        }

        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCount()
        {
            return (from x in doctorRepository.GetAll()
                    group x by x.Specialization into g
                    select new KeyValuePair<string, int>
                    (
                        g.Key, g.Count()
                    )).ToList();
        }

        public IEnumerable<KeyValuePair<string, int>> DoctorSpecializatonCountInSpecificHospital(int hospitalID)
        {
            return (from x in doctorRepository.GetAll()
                    group x by x.Specialization into g
                    select new KeyValuePair<string, int>
                    (
                        g.Key, g.Where(y=>y.HospitalID.Equals(hospitalID)).Count()
                    )).ToList();
        }

        public IEnumerable<Hospital> GetAllBlogs()
        {
            return hospitalRepo.GetAll().ToList();
        }

        public Hospital GetHospitalById(int id)
        {
            return hospitalRepo.Get(id);

            //TODO exception for id
        }

        public IEnumerable<KeyValuePair<string, int>> PatientsPerHospital()
        {


            var asd = from x in patientRepository.GetAll()
                      group x by x.DoctorID into g
                      select new
                      {
                          DOCTOR_ID = g.Key,
                          PAT_NO = g.Count()
                      };

            var kaka = (from x in doctorRepository.GetAll()
                       join z in asd on x.DoctorID equals z.DOCTOR_ID
                       let joinedItem = new { x.DoctorID, x.Name, z.PAT_NO }
                       join y in hospitalRepo.GetAll() on x.HospitalID equals y.HospitalID
                       let yes = new { y.Name, z.PAT_NO }
                       group yes by yes.Name into g
                       select new KeyValuePair<string, int>
                       (
                          g.Key, g.Sum(a => a.PAT_NO)
                       )).ToList();



            

            return kaka;
        }

        public void UpdateHospital(Hospital hospital)
        {
            hospitalRepo.Update(hospital);
        }
    }
}
